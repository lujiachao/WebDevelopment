using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddleWareStudy.Middleware
{
    public interface IGetIp
    {
        Task WriteIp(HttpContext httpContext, ILogger logger);
    }
}
