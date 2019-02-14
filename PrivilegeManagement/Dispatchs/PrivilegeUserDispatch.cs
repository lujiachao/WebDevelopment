using PrivilegeManagement.Arguments;
using PrivilegeManagement.Common.Enum;
using PrivilegeManagement.MiddleWare;
using PrivilegeManagement.Models;
using PrivilegeManagement.Results;
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

        private UserTokenDAL _userTokenDAL = new UserTokenDAL();
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
            var userId = await _userLocalDAL.InsertAsync(
                             new UserLocal
                             {
                                 UserName = arguUserRegister.UserName,
                                 Password = arguUserRegister.PassWord,
                                 PickName = arguUserRegister.PickName,
                                 MobilePhone = arguUserRegister.MobilePhone,
                                 Status = 1
                             }, "id");

           await _userTokenDAL.InsertAsync(
                new UserToken
                {
                    User_Id = userId,
                    Token = Guid.NewGuid().ToString(),
                    Expiration_Time = DateTime.Now.AddDays(3),
                    Display = 1
                });
        }

        public async Task<PrivilegeBaseResult> UserLogin(ArguUserLogin arguUserLogin)
        {
            if (string.IsNullOrWhiteSpace(arguUserLogin.UserName))
            {
                throw new PrivilegeException(EnumPrivilegeException.登录用户名为空,"Login Username is null");
            }

            if (string.IsNullOrWhiteSpace(arguUserLogin.PassWord))
            {
                throw new PrivilegeException(EnumPrivilegeException.登录密码为空,"Login Password is null");
            }
            var userLocal = await _userLocalDAL.CheckUserLogin(arguUserLogin.UserName);
            if (userLocal == null)
            {
                throw new PrivilegeException(EnumPrivilegeException.该用户不存在,"Username is not exist");
            }
            if (arguUserLogin.PassWord != userLocal.passWord)
            {
                throw new PrivilegeException(EnumPrivilegeException.登录密码错误,"Login password error");
            }
            if (userLocal.status == 0)
            {
                throw new PrivilegeException(EnumPrivilegeException.该用户不可用,"User status is false");
            }
            var userInfo = await _userLocalDAL.GetUserInfo(arguUserLogin.UserName);
            if (DateTime.Compare(userInfo.expirationTime,DateTime.Now) <= 0)
            {
               await _userTokenDAL.UpdateToken(Guid.NewGuid().ToString(),DateTime.Now, userInfo.id);
            }
            return await _userLocalDAL.GetUserInfo(arguUserLogin.UserName);
        }

        public async Task<PrivilegeBaseResult> CatchUserList(ArguCatchUser arguCatchUser)
        {
            if (arguCatchUser.id > 0)
            {
                var resultCatchUser = await _userLocalDAL.FindByIDAsync(arguCatchUser.id);
                return Successed(resultCatchUser,"用户信息");
            }
            else
            {
                var resultCatchUser = await _userLocalDAL.GetAllAsync();
                return Successed(resultCatchUser, "用户列表");
            }
        }
    }
}
