using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GetOrdercodeLoop
{
    [Table("tenpay_Result_log")]
    public class TenpayResultLog
    {
        [Key]
        public int id { get; set; }

        public string OrderCode { get; set; }

        public string RequestBody { get; set; }

        public string Memo { get; set; }

        public string RequestContent { get; set; }

        public string ResponseContent { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
