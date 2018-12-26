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
    public class TestMiddleware
    {
        private readonly RequestDelegate _next; //定义请求委托
        public TestMiddleware(RequestDelegate nest) { _next = nest; }

        public async Task InvokeAsync(HttpContext context, IPlayGame game, IServiceProvider provider)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var sv in provider.GetServices<IDemoService>())
            {
                sb.Append($"<br>{sv.Version}<br/>");
            }
            await context.Response.WriteAsync(sb.ToString());
            await _next.Invoke(context);
            //return Task.CompletedTask;
        }
    }
}
