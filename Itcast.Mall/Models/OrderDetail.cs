using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itcast.Mall.Models
{
    [Table("OrderDetail")]
    public class OrderDetail : BaseModel
    {
        public string OrderID { get; set; }

        public int BookID { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
