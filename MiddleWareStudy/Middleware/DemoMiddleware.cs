using Microsoft.AspNetCore.Http;
using MiddleWareStudy.DIContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddleWareStudy.Middleware
{
    public class DemoMiddleware
    {
        public DemoMiddleware(RequestDelegate next) { }

        public async Task InvokeAsync(HttpContext context, IDemoService sv)
        {
            await context.Response.WriteAsync(sv.Version);
        }
    }
}
