using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivilegeManagement.Models
{
    [Table("User_Local")]
    public class UserLocal
    {
        [Key]
        public virtual string ID { get; set; }

        public virtual string UserName { get; set; }

        public virtual string PickName { get; set; }

        public virtual string MobilePhone { get; set; }

        public virtual int Status {get; set;}
    }
}
