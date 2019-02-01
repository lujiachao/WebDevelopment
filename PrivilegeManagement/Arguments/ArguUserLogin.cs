using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivilegeManagement.Arguments
{
    public class ArguUserLogin : BaseArgument
    {
        public string UserName
        {
            get; set;
        }

        public string PassWord
        {
            get; set;
        }
    }
}
