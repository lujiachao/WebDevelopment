using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itcast.Mall.Models
{
    [Table("SensitiveWord")]
    public class SensitiveWord : BaseModel
    {
        public string Original { get; set; }

        public bool IsForbid { get; set; }

        public bool IsReplace { get; set; }

        public string Replace { get; set; }
    }
}
