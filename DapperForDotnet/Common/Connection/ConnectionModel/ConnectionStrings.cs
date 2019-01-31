using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisLibraryTest.Common.Connection.ConnectionModel
{
    public class ConnectionStrings : IOptions<ConnectionStrings>
    {
        public ConnectionStrings Value => this;
        public string mysql
        {
            get; set;
        }

        public string sqlserver
        {
            get; set;
        }

        public string oracle
        {
            get; set;
        }

        public string npgsql
        {
            get; set;
        }

        public string sqlite
        {
            get; set;
        }
    }
}
