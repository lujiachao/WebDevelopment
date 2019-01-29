using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivilegeManagement.Models
{
    [Table("User_Token")]
    public class UserToken
    {
        [Key]
        public virtual string ID { get; set; }

        public virtual string Token { get; set; }

        public virtual DateTime Expiration_Time { get; set; }

        public virtual int Display { get; set; }
    }
}
