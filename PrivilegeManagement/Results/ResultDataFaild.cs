using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivilegeManagement.Results
{
    public class ResultDataFaild : PrivilegeBaseResult
    {
        public string GuidRequest { get; set; }

        public int Code { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}
