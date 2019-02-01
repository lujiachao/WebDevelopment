using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivilegeManagement.Results
{
    public class ResultUserInfo : PrivilegeBaseResult
    {
        public int id
        {
            get; set;
        }
        public string userName
        {
            get; set;
        }

        public string passWord
        {
            get; set;
        }

        public string pickName
        {
            get; set;
        }

        public string mobilePhone
        {
            get; set;
        }
        
        public string token
        {
            get; set;
        }

        public DateTime expirationTime
        {
            get; set;
        }
    }
}
