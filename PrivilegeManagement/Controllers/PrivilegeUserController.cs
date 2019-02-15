using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using PrivilegeManagement.Arguments;
using PrivilegeManagement.Common.Enum;
using PrivilegeManagement.Common.Regular;
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
            await _privilegeUserDispatch.UserRegistre(arguUserRegister);
            return Successed(null);
        }

        [HttpPost]
        public async Task<PrivilegeBaseResult> CheckMobilePhone([FromBody]ArguMobilePhone arguMobilePhone)
        {
            if (arguMobilePhone == null)
            {
                throw new PrivilegeException((int)EnumPrivilegeException.入参为空,"Argument is null,please check Argu");
            }
            if (Regular.IsCorrect(arguMobilePhone.MobilePhone))
            {
                return Successed(arguMobilePhone, "mobile phone number is true");
            }
            else
            {
                return Failed(arguMobilePhone, "Incorrect format of mobile phone number");
            }
        }

        [HttpPost]
        public async Task<PrivilegeBaseResult> UserLogin([FromBody]ArguUserLogin arguUserLogin)
        {
            if (arguUserLogin == null)
            {
                throw new PrivilegeException((int)EnumPrivilegeException.入参为空, "Argument is null,please check Argu");
            }
            return await _privilegeUserDispatch.UserLogin(arguUserLogin);
        }

        [HttpPost]
        public async Task<PrivilegeBaseResult> CatchUserList([FromBody]ArguCatchUser arguCatchUser)
        {
            if (arguCatchUser == null)
            {
                throw new PrivilegeException((int)EnumPrivilegeException.入参为空, "Argument is null,please check Argu");
            }
            return await _privilegeUserDispatch.CatchUserList(arguCatchUser);
        }

        [HttpPost]
        public async Task<PrivilegeBaseResult> UserList([FromBody]ArguUserList arguUserList)
        {
            if (arguUserList == null)
            {
                throw new PrivilegeException((int)EnumPrivilegeException.入参为空, "Argument is null,please check Argu");
            }
            return await _privilegeUserDispatch.UserList(arguUserList);
        }
    }
}
