using Microsoft.AspNetCore.Http;
using MiddleWareStudy.DIContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddleWareStudy.Middleware
{
    public class TestMiddleware
    {
        private readonly RequestDelegate _next; //定义请求委托
        public TestMiddleware(RequestDelegate nest) { _next = nest; }

        public Task InvokeAsync(HttpContext context, IPlayGame game)
        {
            game.Play();
            return Task.CompletedTask;
        }
    }
}
