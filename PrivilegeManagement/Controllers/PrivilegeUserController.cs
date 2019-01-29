using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using PrivilegeManagement.Arguments;
using PrivilegeManagement.Common.Enum;
using PrivilegeManagement.Dispatchs;
using PrivilegeManagement.MiddleWare;
using PrivilegeManagement.Results;
using System.Threading.Tasks;

namespace PrivilegeManagement.Controllers
{
    [EnableCors("AllowCors")]
    [Route("api/privilege/[controller]/[action]")]
    [ApiController]
    public class PrivilegeUserController : PrivilegeController
    {
        private PrivilegeUserDispatch _privilegeUserDispatch = PrivilegeConfigurationProvider.serviceProvider.GetRequiredService<PrivilegeUserDispatch>();

        [HttpPost]
        public async Task<PrivilegeBaseResult> UserRegistre([FromBody]ArguUserRegister arguUserRegister)
        {
            if (arguUserRegister == null)
            {
                throw new PrivilegeException((int)EnumPrivilegeException.入参为空,"Argument is null,please check Argu");
            }

            return Successed(null);
        }
    }
}
