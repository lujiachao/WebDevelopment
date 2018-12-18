using DapperForDotnet.Common.MyEnum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DapperForDotnet.Common.Connection
{
    public interface IConnectionFactory
    {
        IDbConnection CreateConnection();

        DatabaseType GetDataBaseType(string databaseType);
    }
}
