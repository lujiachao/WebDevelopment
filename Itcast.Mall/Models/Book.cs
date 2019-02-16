using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itcast.Mall.Models
{
    [Table("Book")]
    public class Book : BaseModel
    {
        public int CategoryId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public int PublishDate { get; set; }

        public string ISBN { get; set; }

        public int WordsCount { get; set; }

        public decimal UnitPrice { get; set; }

        public string ContentDescription { get; set; }

        public string AurhorDescription { get; set; }

        public string EditorComment { get; set; }

        public string TOC { get; set; }
    }
}
