using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace Zeus.Controllers
{
    public abstract class ZeusController : ControllerBase, IActionFilter, IAsyncActionFilter
    {
        [NonAction]
        public virtual async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            OnActionExecuting(context);
            if (context.Result == null)
            {
                OnActionExecuted(await next());
            }


        }

        [NonAction]
        public virtual void OnActionExecuting(ActionExecutingContext context)
        {
        }


        [NonAction]
        public virtual void OnActionExecuted(ActionExecutedContext context)
        {
        }

    }
}
