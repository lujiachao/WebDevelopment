using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PrivilegeManagement.Common.Regular
{
    public class Regular
    {
        #region 验证正则
        /// <summary>
        /// 验证手机号的正确性
        /// </summary>
        public static bool IsCorrect(string mobilePhone)
        {
            return Regex.IsMatch(mobilePhone,@"^[1][3,4,5,7,8][0-9]{9}$");
        }
        #endregion
    }
}
