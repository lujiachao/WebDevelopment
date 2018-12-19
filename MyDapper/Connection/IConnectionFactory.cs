using MyDapper.Connection.MyEnum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MyDapper.Connection
{
    public interface IConnectionFactory
    {
        IDbConnection CreateConnection();

        DatabaseType GetDataBaseType(string databaseType);
    }
}
