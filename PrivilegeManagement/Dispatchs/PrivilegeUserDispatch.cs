using PrivilegeManagement.Arguments;
using PrivilegeManagement.Common.Enum;
using PrivilegeManagement.MiddleWare;
using PrivilegeManagement.Models;
using PrivilegeManagement.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivilegeManagement.Dispatchs
{
    public class PrivilegeUserDispatch : BaseDispatch
    {
        private UserLocalDAL _userLocalDAL = new UserLocalDAL();
        public async Task UserRegistre(ArguUserRegister arguUserRegister)
        {
            if (string.IsNullOrWhiteSpace(arguUserRegister.UserName))
            {
                throw new PrivilegeException(EnumPrivilegeException.注册用户名为空,"Register Username is null");
            }

            if (string.IsNullOrWhiteSpace(arguUserRegister.PassWord))
            {
                throw new PrivilegeException(EnumPrivilegeException.注册密码不能为空,"Register Password is null");
            }

            if (string.IsNullOrWhiteSpace(arguUserRegister.PickName))
            {
                throw new PrivilegeException(EnumPrivilegeException.注册昵称不可为空,"Register Nickname is null");
            }

            if (string.IsNullOrWhiteSpace(arguUserRegister.MobilePhone))
            {
                throw new PrivilegeException(EnumPrivilegeException.手机号不可为空,"Register Mobilephone is null");
            }

            if (!await _userLocalDAL.CheckUsernameExist(arguUserRegister.UserName))
            {
                throw new PrivilegeException(EnumPrivilegeException.用户名已存在,"Register Username is exist");
            }

            if (!await _userLocalDAL.CheckMobilephoneExist(arguUserRegister.MobilePhone))
            {
                throw new PrivilegeException(EnumPrivilegeException.该手机号已存在,"Register Mobilephone is exist");
            }
            await _userLocalDAL.InsertAsync(
                new UserLocal
                {
                    UserName = arguUserRegister.UserName,
                    Password = arguUserRegister.PassWord,
                    PickName = arguUserRegister.PickName,
                    MobilePhone = arguUserRegister.MobilePhone,
                    Status = 1
                });
        }
    }
}
