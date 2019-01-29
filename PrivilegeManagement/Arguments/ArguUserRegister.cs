using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivilegeManagement.Arguments
{
    public class ArguUserRegister : BaseArgument
    {
        public string UserName
        {
            get; set;
        }

        public string PassWord
        {
            get; set;
        }

        public string PickName
        {
            get; set;
        }

        public string MobilePhone
        {
            get; set;
        }

        public int Status
        {
            get; set;
        }
    }
}
