using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zeus.Results;

namespace Zeus.Filters
{
    public class ZeusExceptionFilter : IExceptionFilter, IAsyncExceptionFilter
    {
        public virtual void OnException(ExceptionContext exceptionContext)
        {
            exceptionContext.Result = new ZeusResult(exceptionContext.Exception);
            exceptionContext.ExceptionHandled = true;
        }

        public virtual Task OnExceptionAsync(ExceptionContext exceptionContext)
        {
            OnException(exceptionContext);
            return Task.CompletedTask;
        }
    }
}
