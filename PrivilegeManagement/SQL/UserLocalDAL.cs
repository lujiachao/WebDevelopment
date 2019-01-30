using MyDapper.SqlPower;
using PrivilegeManagement.Models;
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
    }
}
