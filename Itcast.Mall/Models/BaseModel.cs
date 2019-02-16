using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itcast.Mall.Models
{
    public class BaseModel
    {
        [Key]
        public int Id
        {
            get; set;
        }
    }
}
