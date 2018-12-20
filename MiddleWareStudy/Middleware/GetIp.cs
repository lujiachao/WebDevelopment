using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddleWareStudy.Middleware
{
    public class GetIp : IGetIp
    {
        public async Task WriteIp(HttpContext httpContext, ILogger logger)
        {
            string ip = httpContext.Connection.RemoteIpAddress.ToString();

            logger.LogInformation($"User IP:{ip}");
        }
    }
}
