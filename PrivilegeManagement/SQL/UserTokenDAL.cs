using MyDapper.SqlPower;
using PrivilegeManagement.Models;
using System;
using System.Threading.Tasks;

namespace PrivilegeManagement.SQL
{
    public class UserTokenDAL : BaseDAL<UserToken>
    {
        public async Task UpdateToken(string token,DateTime expiration_time,int id)
        {
            var commandText = $"Update {EntityHelper.CallName<UserToken>()} set token = @token, expiration_time = @expiration_time where id = @id";
            await ExecuteAsync(commandText, new { token, expiration_time, id});
        }
    }
}
