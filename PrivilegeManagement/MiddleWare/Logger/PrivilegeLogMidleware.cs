using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using PrivilegeManagement.Models;
using PrivilegeManagement.SQL;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PrivilegeManagement.MiddleWare.Logger
{
    public class PrivilegeLogMidleware
    {
        private readonly RequestDelegate _next; //定义请求委托
        private readonly ILogger _logger; //定义日志
        private StreamReader _streamReader;
        private Stopwatch _stopwatch;
        private DateTime _timeCreate;

        public PrivilegeLogMidleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            string remoteAddress = context.Connection.RemoteIpAddress.ToString();
            remoteAddress = remoteAddress == "::1" ? "127.0.0.1" : remoteAddress;
            string forwarderAddress = context.Request.Headers["X-Forwarded-For"];
            _streamReader = new StreamReader(context.Request.Body);
            string requestStr = await _streamReader.ReadToEndAsync();
            string host = context.Request.Host.ToString();
            string uri = UriHelper.GetDisplayUrl(context.Request);
            var traceIdentifier = context.TraceIdentifier;
            _timeCreate = new DateTime();
            _timeCreate = DateTime.Now;
            var headers = BuildHeader(context.Request.Headers);
            var newResponseBodyStream = new MemoryStream();
            //context.Response.Body = newResponseBodyStream;    //bug代码，用了就获取不到接口返回结果了
            await _next.Invoke(context);
            newResponseBodyStream.Seek(0, SeekOrigin.Begin);
            var responseBodyText = new StreamReader(newResponseBodyStream, Encoding.UTF8).ReadToEnd();
            _stopwatch.Stop();
            var dissipate = _stopwatch.ElapsedMilliseconds;
            HttpRequestLogDAL httpRequestLogDAL = new HttpRequestLogDAL();
            var httpRequestLog = new HttpRequestLog()
            {
                RemoteAddress = remoteAddress,
                ForwarderAddress = forwarderAddress,
                Host = host,
                Uri = uri,
                TraceIdentiier = traceIdentifier,
                Token = null,
                RequestBody = requestStr,
                TimeCreate = _timeCreate,
                Headers = headers,
                ResponseBody = responseBodyText,
                Dissipate = dissipate
            };
            // 屏蔽swagger请求，方法有待提升
            if (!uri.Contains("swagger/index.html"))
            {
                await httpRequestLogDAL.InsertAsync(httpRequestLog);
            }
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
}
