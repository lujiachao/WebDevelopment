using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using MiddleWareStudy.DAL;
using MiddleWareStudy.Model;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareStudy.LoggersMiddleware
{
    public class RequestLogMiddleware
    {
        private readonly RequestDelegate _next; //定义请求委托
        private readonly ILogger _logger; //定义日志
        private StreamReader streamReader;


        public RequestLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            streamReader = new StreamReader(context.Request.Body);
            string remoteAddress = context.Connection.RemoteIpAddress.ToString();
            if (remoteAddress == "::1")
            {
                remoteAddress = "127.0.0.1";
            }
            string forwarderAddress = context.Request.Headers["X-Forwarded-For"];
            string requestStr = await streamReader.ReadToEndAsync();
            StringBuilder sb = new StringBuilder();
            string host = context.Request.Host.ToString();
            sb.Append($"1.{context.Request.Host.ToString()}/n/r");                 //主机地址
            string uri = UriHelper.GetDisplayUrl(context.Request);
            sb.Append($"2.{UriHelper.GetDisplayUrl(context.Request)}/n/r");        //请求url
            var traceIdentifier = context.TraceIdentifier;
            sb.Append($"3.{context.TraceIdentifier}/n/r");                         //请求唯一标识
            sb.Append($"4.Token暂时没有");                                         //token
            sb.Append($"5.{requestStr}/n/r");                                      //请求body
            sb.Append($"6.用户id或者标识/n/r");                                    //用户id或者标识
            DateTime timeCreate = DateTime.Now;                                    //请求时间
            var headers = BuildHeader(context.Request.Headers);
            Console.WriteLine("dsadasdas");
            var newResponseBodyStream = new MemoryStream();
            context.Response.Body = newResponseBodyStream;                                           
            await _next(context);                                                  //继续执行中间件
            newResponseBodyStream.Seek(0, SeekOrigin.Begin);
            var responseBodyText = new StreamReader(newResponseBodyStream, Encoding.UTF8).ReadToEnd();   //获取请求的response
            stopwatch.Stop();
            var dissipate = stopwatch.ElapsedMilliseconds;
            HttpRequestLogDAL httpRequestLog = new HttpRequestLogDAL();
            var insertInfo = new HttpRequestLog()
            {
                RemoteAddress = remoteAddress,
                ForwarderAddress = forwarderAddress,
                Host = host,
                Uri = uri,
                TraceIdentiier = traceIdentifier,
                Token = null,
                RequestBody = requestStr,
                TimeCreate = timeCreate,
                Headers = headers,
                ResponseBody = responseBodyText,
                Dissipate = dissipate
            };
            await httpRequestLog.InsertAsync(insertInfo);

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

        private async Task<string> ReadBodyAsync(HttpContext context)
        {
            var newResponseBodyStream = new MemoryStream();
            context.Response.Body = newResponseBodyStream;
            await _next(context);                                                  //继续执行中间件,错误用法，无法进入接口
            newResponseBodyStream.Seek(0, SeekOrigin.Begin);
            var responseBodyText = new StreamReader(newResponseBodyStream, Encoding.UTF8).ReadToEnd();
            return responseBodyText;
        }
    }
}
