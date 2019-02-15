using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivilegeManagement.Arguments
{
    public class ArguUserList
    {
        public int Page
        {
            get; set;
        }

        public int Limit
        {
            get; set;
        }

        public string UserName
        {
            get; set;
        }

        public string PickName
        {
            get; set;
        }

        public string Mobilephone
        {
            get; set;
        }

        public int? Status
        {
            get; set;
        }
    }
}
