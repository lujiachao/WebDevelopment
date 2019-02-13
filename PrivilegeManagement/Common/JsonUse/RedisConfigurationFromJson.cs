using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivilegeManagement.Common.JsonUse
{
    public class RedisConfigurationFromJson
    {
        public string ServiceName
        {
            get; set;
        }

        public string ConnectionString
        {
            get; set;
        }

        public int DatabaseId
        {
            get; set;
        }

        public int SyncTimeOut
        {
            get; set;
        }

        public string Password
        {
            get; set;
        }
    }
}
