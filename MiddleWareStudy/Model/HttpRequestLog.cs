using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddleWareStudy.Model
{
    [Table("Http_Request_Log")]
    public class HttpRequestLog
    {
        [Key] //自增长的用此标识
        public virtual string ID { get; set; }

        public virtual string RemoteAddress { get; set; }

        public virtual string ForwarderAddress { get; set; }

        public virtual string Host { get; set; }

        public virtual string Uri { get; set; }

        public virtual string TraceIdentiier { get; set; }

        public virtual string Token { get; set; }

        public virtual string RequestBody { get; set; }

        public virtual DateTime TimeCreate { get; set; }

        public virtual string Headers { get; set; }

        public virtual string ResponseBody { get; set; }

        public virtual long Dissipate { get; set; }
    }
}
