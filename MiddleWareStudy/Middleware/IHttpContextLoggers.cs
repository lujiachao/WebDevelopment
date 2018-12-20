using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddleWareStudy.Middleware
{
    public interface IHttpContextLoggers : IDisposable
    {
        Task BuildRequestLogger(HttpContext httpContext);

        Task BuildResponseLogger(HttpContext httpContext);

        Task BuildExceptionLogger(HttpContext httpContext);

    }
}
