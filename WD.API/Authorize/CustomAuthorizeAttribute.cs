using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace WD.API.Authorize
{
    ///<summary>
    /// ActionFilterAttribute
    ///</summary>
    public class CustomAuthorizeAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Do Something
        }
    }

    ///<summary>
    /// AuthorizeAttribute
    ///</summary>
    public class customAuthorizeAttribute : AuthorizeAttribute
    {
    }

    public class TestAuthorizationAttribute : Attribute,IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
        }
    }
}
