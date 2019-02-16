using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itcast.Mall.Models
{
    [Table("Option")]
    public class Option : BaseModel
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public bool Enabled { get; set; }
    }
}
