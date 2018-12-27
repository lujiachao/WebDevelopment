using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using System.IO;
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

        public async Task Invoke(HttpContext context)
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
            sb.Append($"5.{requestStr}/n/r");                                             //请求body
            sb.Append($"6.用户id或者标识/n/r");                                    //用户id或者标识

        }
    }
}
