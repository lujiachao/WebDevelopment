using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itcast.Mall.Models
{
    [Table("User")]
    public class User : BaseModel
    {
        public string Login { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Password { get; set; }

        public string Nickname { get; set; }

        public int Status { get; set; }
    }
}
