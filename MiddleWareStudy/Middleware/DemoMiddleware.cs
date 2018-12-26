using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MiddleWareStudy.DIContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareStudy.Middleware
{
    public class DemoMiddleware
    {
        private readonly RequestDelegate _next; //定义请求委托
        private object provider;

        public DemoMiddleware(RequestDelegate next) { _next = next; }

        public async Task InvokeAsync(HttpContext context, IEnumerable<IDemoService> svs)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var sv in svs)
            {
                sb.Append($"<br>{sv.Version}<br/>");
            }
            await context.Response.WriteAsync(sb.ToString());
            await _next.Invoke(context);
        }
    }
}
