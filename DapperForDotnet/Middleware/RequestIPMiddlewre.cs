using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace RedisLibraryTest.Middleware
{
    /// <summary>
    /// 请求IP中间件
    /// </summary>
    public class RequestIPMiddlewre
    {
        private readonly RequestDelegate _next; //定义请求委托
        private readonly ILogger _logger; //定义日志

        public RequestIPMiddlewre(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestIPMiddlewre>();
        }

        ///<summary>
        ///中间件核心功能
        /// 做什么功能，都在该方法中处理
        /// </summary>
        /// <param name="context"></param>
        /// <returns>Task(void)</returns>
        public async Task Invoke(HttpContext context)
        {
            string ip = context.Connection.RemoteIpAddress.ToString();
            _logger.LogInformation($"User IP:{ip}");
            await _next.Invoke(context);
        }
        
    }
}
