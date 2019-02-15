using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using PrivilegeManagement.Common.Enum;
using PrivilegeManagement.Controllers;
using PrivilegeManagement.Dispatchs;
using PrivilegeManagement.MiddleWare;
using PrivilegeManagement.SQL;
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

        public TokenFilter()
        {

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
                UserTokenDAL userTokenDAL = new UserTokenDAL();
                var userToken = await userTokenDAL.FindToken(token);
                if (userToken == null)
                {
                    throw new PrivilegeException((int)EnumPrivilegeException.用户身份令牌不存在,"token is error");
                }
                if (DateTime.Compare(userToken.Expiration_Time, DateTime.Now) <= 0)
                {
                    throw new PrivilegeException((int)EnumPrivilegeException.用户身份令牌过期, "token is expiration");
                }
                await next.Invoke();
            }
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
