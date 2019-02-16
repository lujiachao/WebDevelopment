using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itcast.Mall.Models
{
    [Table("Publisher")]
    public class Publisher : BaseModel
    {
        public string Name { get; set; }
    }
}
