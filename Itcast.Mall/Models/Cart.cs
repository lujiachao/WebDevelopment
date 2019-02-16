using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itcast.Mall.Models
{
    [Table("Cart")]
    public class Cart : BaseModel
    {
        public int UserId { get; set; }

        public int BookId { get; set; }

        public int Count { get; set; }
    }
}
