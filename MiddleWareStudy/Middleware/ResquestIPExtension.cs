using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddleWareStudy.Middleware
{
    public static class ResquestIPExtension
    {
        ///<summary>
        /// 封装依赖注入
        /// </summary>
        /// <param name="builder">扩展类型</param>
        /// <returns>IApplicationBuilder</returns>
        public static IServiceCollection UseRequestIP(this IServiceCollection collecction)
        {
            return collecction.AddTransient<IGetIp,GetIp>();
        }

        ///<summary>
        ///引入请求IP中间件
        /// </summary>
        /// <param name="builder">扩展类型</param>
        /// <returns>IApplicationBuilder</returns>
        public static IApplicationBuilder UseRequestIP(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestIPMiddleware>();
        }
    }
}
