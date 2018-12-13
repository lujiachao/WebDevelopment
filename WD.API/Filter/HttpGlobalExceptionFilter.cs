using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Zeus.Filters;

namespace WD.API.Filter
{
    public class HttpGlobalExceptionFilter : IExceptionFilter, IAsyncExceptionFilter
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IHostingEnvironment _env;

        public HttpGlobalExceptionFilter(ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            _loggerFactory = loggerFactory;
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {
            var logger = _loggerFactory.CreateLogger(context.Exception.TargetSite.ReflectedType);
            logger.LogError(new EventId(context.Exception.HResult),context.Exception,context.Exception.Message);

            var json = new ErrorResponse("未知错误，请重试！");
        }

        public virtual Task OnExceptionAsync(ExceptionContext exceptionContext)
        {
            OnException(exceptionContext);
            return Task.CompletedTask;
        }

    }

    public class ErrorResponse
    {
        public ErrorResponse(string msg)
        {
            Message = msg;
        }

        public string Message { get; set; }
        public object DeveloperMessage { get; set; }
    }
}
