using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareStudy.Middleware
{
    public class TestMiddlewareNext
    {
        private readonly RequestDelegate _next; //定义请求委托
        private readonly ILogger _logger;
        public TestMiddlewareNext(RequestDelegate nest, ILogger<TestMiddleware> logger) { _next = nest; _logger = logger; }

        public async Task InvokeAsync(HttpContext context)
        {

            _logger.LogInformation("中间件开始");
            await _next(context);
            _logger.LogInformation("中间件完成");
        }
    }
}
