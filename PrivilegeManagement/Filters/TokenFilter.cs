using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using PrivilegeManagement.Common.Enum;
using PrivilegeManagement.Controllers;
using PrivilegeManagement.Dispatchs;
using PrivilegeManagement.MiddleWare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivilegeManagement.Filters
{
    public class TokenFilter : Attribute, IAsyncActionFilter, IActionFilter
    {
        public const string Privilege_TOKEN = "Privilege_TOKEN";

        public EnumTokenType _enumTokenType;

        public TokenFilter(EnumTokenType enumTokenType)
        {
            _enumTokenType = enumTokenType;
        }


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context == null)
            {
                throw new PrivilegeException((int)EnumPrivilegeException.请求上下文为空,"context is null");
            }
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }
            //如果不是允许任意模式访问的接口
            if (!context.Filters.Any(item => item is IAllowAnonymousFilter))
            {
                var token = context.HttpContext.Request.Headers[Privilege_TOKEN];
                if (string.IsNullOrWhiteSpace(token))
                {
                    throw new PrivilegeException((int)EnumPrivilegeException.未查询到该身份,"token not found");
                }
            }
            var apiController = context.Controller as PrivilegeController;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
