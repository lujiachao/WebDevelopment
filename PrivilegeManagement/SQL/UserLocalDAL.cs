using MyDapper.SqlPower;
using PrivilegeManagement.Arguments;
using PrivilegeManagement.Models;
using PrivilegeManagement.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrivilegeManagement.SQL
{
    public class UserLocalDAL : BaseDAL<UserLocal>
    {
        public async Task<bool> CheckUsernameExist(string userName)
        {
            var commandText = $"select count(1) from {EntityHelper.CallName<UserLocal>()} where username = @username";
            var count = await ExecuteScalarAsync<int>(commandText, new { username = userName });
            if (count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<bool> CheckMobilephoneExist(string mobilePhone)
        {
            var commandText = $"select count(1) from {EntityHelper.CallName<UserLocal>()} where mobilephone = @mobilephone";
            var count = await ExecuteScalarAsync<int>(commandText, new { mobilePhone = mobilePhone});
            if (count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<ResultUserLocal> CheckUserLogin(string userName)
        {
            var commandText = $"select * from {EntityHelper.CallName<UserLocal>()} where username = @username";
            var resultUserLocal = await QueryOneAsync<ResultUserLocal>(commandText, new { userName});
            return resultUserLocal;
        }

        public async Task<ResultUserInfo> GetUserInfo(string userName)
        {
            var commandText = $"select * from {EntityHelper.CallName<UserLocal>()} A left join {EntityHelper.CallName<UserToken>()} B ON A.id = B.user_id where A.username = @username";
            var resultUserInfo = await QueryOneAsync<ResultUserInfo>(commandText, new { userName});
            return resultUserInfo;
        }

        public async Task<IEnumerable<UserLocal>> GatchUserListByID(int id)
        {
            var commandText = $"select * from {EntityHelper.CallName<UserLocal>()} where id = @id";
            var resultUserLocal = await QueryAsync<UserLocal>(commandText, new { id });
            return resultUserLocal;
        }

        public async Task<IEnumerable<UserLocal>> UserList(ArguUserList arguUserList)
        {
            int limitBefore = (arguUserList.Page - 1) * arguUserList.Limit;
            string wheresql = $" where 1 = 1";
            if (!string.IsNullOrWhiteSpace(arguUserList.UserName))
            {
                wheresql += $" and username like '%{arguUserList.UserName}%' ";
            }
            if (!string.IsNullOrWhiteSpace(arguUserList.Mobilephone))
            {
                wheresql += $" and mobnilephone = '{arguUserList.Mobilephone}' ";
            }
            if (!string.IsNullOrWhiteSpace(arguUserList.PickName))
            {
                wheresql += $" and pickname like '{arguUserList.PickName}'";
            }
            if (arguUserList.Status != null)
            {
                wheresql += $" and status = {arguUserList.Status}";
            }
            var commandText = $"select * from {EntityHelper.CallName<UserLocal>()} {wheresql} limit {limitBefore},{arguUserList.Limit}";
            var userList = await QueryAsync<UserLocal>(commandText , null);
            return userList;
        }
    }
}
