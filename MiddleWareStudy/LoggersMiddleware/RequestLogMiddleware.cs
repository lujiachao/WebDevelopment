using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareStudy.LoggersMiddleware
{
    public class RequestLogMiddleware
    {
        private readonly RequestDelegate _next; //定义请求委托
        private readonly ILogger _logger; //定义日志

        public RequestLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"1.{context.Request.Host.ToString()}/n/r");
            sb.Append($"2.{UriHelper.GetDisplayUrl(context.Request)}/n/r");
            sb.Append($"3.{context}/n/r");
        }
    }
}
