using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itcast.Mall.Models
{
    [Table("Comment")]
    public class Comment : BaseModel
    {
        public string Content{get; set;}

        public DateTime Created { get; set; }

        public int BookId { get; set; }
    }
}
