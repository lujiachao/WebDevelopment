using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PrivilegeManagement.Core
{
    interface IConnectionFactory
    {
        IDbConnection CreateConnection();

        DatabaseType GetDataBaseType(string databaseType);
    }
}
