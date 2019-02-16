using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itcast.Mall.Models
{
    [Table("Captcha")]
    public class Captcha : BaseModel
    {
        public int UserId { get; set; }

        public string Token { get; set; }

        public bool Actived { get; set; }

        public DateTime Expired { get; set; }
    }
}
