using System;
using System.Configuration;
using System.Data.Common;
using System.Threading.Tasks;

using Autofac;
namespace Gaea
{
    using SuperPower;
    public partial class DefaultGaeaConnectionFactory : IGaeaConnectionFactory
    {
        public DbConnection GetConnection()
        {
            if (_dbConnectionFactoryCore != null)
            {
                return _dbConnectionFactoryCore();
            }
            if (_container == null)
            {
                throw new Exception("PLEASE RESIGER FACTORY FIRST!");
            }

            var dbConnection = _container.Resolve<DbConnection>();
            dbConnection.ConnectionString = ConnectionString;
            dbConnection.Open();
            return dbConnection;
        }
    }
}
