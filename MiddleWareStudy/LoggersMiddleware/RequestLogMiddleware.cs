using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using System;
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
            streamReader = new StreamReader(context.Request.Body);
            string RemoteAddress = context.Connection.RemoteIpAddress.ToString();
            string ForwarderAddress = context.Request.Headers["X-Forwarded-For"];
            string requestStr = await streamReader.ReadToEndAsync();
            StringBuilder sb = new StringBuilder();
            sb.Append($"1.{context.Request.Host.ToString()}/n/r");                 //主机地址
            sb.Append($"2.{UriHelper.GetDisplayUrl(context.Request)}/n/r");        //请求url
            sb.Append($"3.{context.TraceIdentifier}/n/r");                         //请求唯一标识
            sb.Append($"4.Token暂时没有");                                         //token
            sb.Append($"5.{requestStr}/n/r");                                      //请求body
            sb.Append($"6.用户id或者标识/n/r");                                    //用户id或者标识
            DateTime TimeCreate = DateTime.Now;                                    //请求时间
            var Headers = BuildHeader(context.Request.Headers);
            Console.WriteLine("dsadasdas");
            var newResponseBodyStream = new MemoryStream();
            context.Response.Body = newResponseBodyStream;
            await _next(context);                                                  //继续执行中间件
            newResponseBodyStream.Seek(0, SeekOrigin.Begin);
            var responseBodyText = new StreamReader(newResponseBodyStream, Encoding.UTF8).ReadToEnd();
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
