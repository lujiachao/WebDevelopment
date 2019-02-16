using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itcast.Mall.Models
{
    [Table("Order")]
    public class Order : BaseModel
    {
        public string OrderId { get; set; }

        public DateTime OrderData { get; set; }

        public int UserId { get; set; }

        public decimal TotalPrice { get; set; }

        public string PostAddress { get; set; }

        public int State { get; set; }
    }
}
