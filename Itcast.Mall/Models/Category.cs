using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itcast.Mall.Models
{
    [Table("Category")]
    public class Category : BaseModel
    {
        public string Name { get; set; }

        public int ParentId { get; set; }
    }
}
