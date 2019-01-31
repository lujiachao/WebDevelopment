using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisLibraryTest.Middleware
{
    public static class RequestIPExtensions
    {
        ///<summary>
        ///引入请求IP中间件
        /// </summary>
        /// <param name="builder">扩展类型</param>
        /// <returns>IApplicationBuilder</returns>
        public static IApplicationBuilder UseRequestIP(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestIPMiddlewre>();
        }
    }
}
