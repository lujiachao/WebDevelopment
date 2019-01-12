using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using MiddleWareStudy.DAL;
using MiddleWareStudy.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiddleWareStudy.LoggersMiddleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private Stopwatch _stopWatch;
        private HttpRequestLog _httpRequestLog;

        public RequestLoggingMiddleware(RequestDelegate next,
            ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;

        }

        public async Task Invoke(HttpContext context)
        {
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            string remoteAddress = context.Connection.RemoteIpAddress.ToString();                       //远程访问地址
            if (remoteAddress == "::1")
            {
                remoteAddress = "127.0.0.1";
            }
            string forwarderAddress = context.Request.Headers["X-Forwarded-For"];                      //forwarderAddress
            //context.TraceIdentifier = Guid.NewGuid().ToString("N");
            var request = context.Request;
            await RequestEnableRewindAsync(request).ConfigureAwait(false);
            var encoding = GetRequestEncoding(request);
            var requestContent = await ReadStreamAsync(request.Body, encoding).ConfigureAwait(false);
            //WritLog(context, JsonConvert.SerializeObject(requestContent));
            var host = context.Request.Host.ToString();                                                //主机地址
            var uri = UriHelper.GetDisplayUrl(context.Request);                                        //访问url
            var traceIdentifier = context.TraceIdentifier;                                             //请求唯一标识
            DateTime timeCreate = DateTime.Now;                                                        //服务器接收到请求的时间
            var headers = BuildHeader(context.Request.Headers);                                        //请求headers
            await ResponseEnableRewindAsync(context.Response);
            context.Response.OnCompleted(ResponseCompletedCallback, context);
            await _next(context);
            _stopWatch.Stop();
            var dissipate = _stopWatch.ElapsedMilliseconds;                                            //单次请求消耗时间，以毫秒计算               
            //将获取到的内容保存在实例中
            _httpRequestLog = new HttpRequestLog();
            _httpRequestLog.RemoteAddress = remoteAddress;
            _httpRequestLog.ForwarderAddress = forwarderAddress;
            _httpRequestLog.Host = host;
            _httpRequestLog.Uri = uri;
            _httpRequestLog.TraceIdentiier = traceIdentifier;
            _httpRequestLog.Token = null;
            _httpRequestLog.RequestBody = requestContent;
            _httpRequestLog.TimeCreate = timeCreate;
            _httpRequestLog.Headers = headers;
            _httpRequestLog.Dissipate = dissipate;
        }

        private async Task ResponseCompletedCallback(object obj)
        {
            if (obj is HttpContext context)
            {
                var response = await ResponseReadStreamAsync(context.Response);　　  　　 　　   //记录日志
                //ExceptionlessClient.Default.CreateLog(JsonConvert.SerializeObject(response)).AddTags(context.TraceIdentifier).Submit();
                _httpRequestLog.ResponseBody = response;
                await WriteLog(_httpRequestLog);
            }

        }
        private async Task<string> ResponseReadStreamAsync(HttpResponse response)
        {
            if (response.Body.Length > 0)
            {
                response.Body.Seek(0, SeekOrigin.Begin);
                var encoding = GetEncoding(response.ContentType);
                var retStr = await ReadStreamAsync(response.Body, encoding, false).ConfigureAwait(false);
                return retStr;
            }
            return null;
        }
        private async Task<string> ReadStreamAsync(Stream stream, Encoding encoding, bool forceSeekBeginZero = true)
        {
            using (StreamReader sr = new StreamReader(stream, encoding, true, 1024, true))//这里注意Body部分不能随StreamReader一起释放
            {
                var str = await sr.ReadToEndAsync();
                if (forceSeekBeginZero)
                {
                    stream.Seek(0, SeekOrigin.Begin);//内容读取完成后需要将当前位置初始化，否则后面的InputFormatter会无法读取
                }
                return str;
            }
        }

        //private void WritLog(HttpContext context, string body)
        //{
        //    var request = context.Request;
        //    try
        //    {
        //        Task.Factory.StartNew(() =>
        //        {
        //            LogMode logMode = new LogMode
        //            {
        //                Body = body,
        //                CententLength = request.ContentLength,
        //                CententType = request.ContentType,
        //                Headers = JsonConvert.SerializeObject(request.Headers),
        //                Host = request.Host.Host,
        //                Method = request.Method,
        //                Path = request.Path,
        //                Query = JsonConvert.SerializeObject(request.Query)
        //            };　　　　　　　　　 // 记录日志
        //           // ExceptionlessClient.Default.CreateLog(JsonConvert.SerializeObject(logMode)).AddTags(context.TraceIdentifier).Submit();
        //        });
        //    }
        //    catch (Exception ex)
        //    {　　　　　　　　　//记录日志
        //         //ex.ToExceptionless().Submit();
        //    }

        //}

        private async Task WriteLog(HttpRequestLog httpRequestLog)
        {
            HttpRequestLogDAL httpRequestLogDAL = new HttpRequestLogDAL();
            await httpRequestLogDAL.InsertAsync(httpRequestLog);
        }

        private async Task ResponseEnableRewindAsync(HttpResponse response)
        {
            if (!response.Body.CanRead || !response.Body.CanSeek)
            {
                response.Body = new MemoryWrappedHttpResponseStream(response.Body);
            }
        }

        private async Task RequestEnableRewindAsync(HttpRequest request)
        {
            if (!request.Body.CanSeek)
            {
                request.EnableBuffering();

                await request.Body.DrainAsync(CancellationToken.None);
                request.Body.Seek(0L, SeekOrigin.Begin);
            }
        }

        private async Task<string> ReadStreamAsync(Stream stream, Encoding encoding)
        {
            using (StreamReader sr = new StreamReader(stream, encoding, true, 1024, true))//这里注意Body部分不能随StreamReader一起释放
            {
                var str = await sr.ReadToEndAsync();
                stream.Seek(0, SeekOrigin.Begin);//内容读取完成后需要将当前位置初始化，否则后面的InputFormatter会无法读取
                return str;
            }
        }

        private Encoding GetRequestEncoding(HttpRequest request)
        {
            var requestContentType = request.ContentType;
            var requestMediaType = requestContentType == null ? default(MediaType) : new MediaType(requestContentType);
            var requestEncoding = requestMediaType.Encoding;
            if (requestEncoding == null)
            {
                requestEncoding = Encoding.UTF8;
            }
            return requestEncoding;
        }


        private Encoding GetEncoding(string contentType)
        {
            var mediaType = contentType == null ? default(MediaType) : new MediaType(contentType);
            var encoding = mediaType.Encoding;
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            return encoding;
        }

        public virtual string BuildHeader(IHeaderDictionary keyValuePairs)
        {
            var stringBuilder = new StringBuilder();
            foreach (var item in keyValuePairs)
            {
                stringBuilder.Append(item.Key + "=" + item.Value + ";");
            }
            return stringBuilder.ToString();
        }
    }
    public class MemoryWrappedHttpResponseStream : MemoryStream
    {
        private Stream _innerStream;
        public MemoryWrappedHttpResponseStream(Stream innerStream)
        {
            this._innerStream = innerStream ?? throw new ArgumentNullException(nameof(innerStream));
        }
        public override void Flush()
        {
            this._innerStream.Flush();
            base.Flush();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            base.Write(buffer, offset, count);
            this._innerStream.Write(buffer, offset, count);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                this._innerStream.Dispose();
            }
        }

        public override void Close()
        {
            base.Close();
            this._innerStream.Close();
        }
    }
}
