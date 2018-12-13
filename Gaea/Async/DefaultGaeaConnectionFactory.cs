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
        private static IContainer _container;
        private static string _connectionStringConfigureName;
        private static Func<DbConnection> _dbConnectionFactoryCore;
        private static string _connectionString;


        public static string ConnectionString {
            get {

                if (string.IsNullOrWhiteSpace(_connectionString))
                {
                    _connectionString = ConfigurationManager.ConnectionStrings[_connectionStringConfigureName].ConnectionString;
                }

                return _connectionString;
            }
            set {
                _connectionString = value;
            }
        }

        public static void RegisterByConfiguration(IContainer container, string connectionStringConfigureName, string nameSymbo = "", bool enableGaeaSuperName = true, Func<DbConnection> dbConnectionFactoryCore = null)
        {
            _container = container;
            _connectionStringConfigureName = connectionStringConfigureName;
            GaeaSuperPower.InitNameSymbol(nameSymbo);
            if (enableGaeaSuperName)
            {
                GaeaSuperPower.EnableGaeaSuperName();
            }
            _dbConnectionFactoryCore = dbConnectionFactoryCore;
        }

        public static void Register(IContainer container, string connectionString, string nameSymbo = "", bool enableGaeaSuperName = true, Func<DbConnection> dbConnectionFactoryCore = null)
        {
            _container = container;
            _connectionString = connectionString;
            GaeaSuperPower.InitNameSymbol(nameSymbo);
            if (enableGaeaSuperName)
            {
                GaeaSuperPower.EnableGaeaSuperName();
            }
            _dbConnectionFactoryCore = dbConnectionFactoryCore;
        }

        public async Task<DbConnection> GetConnectionAsync()
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
            await dbConnection.OpenAsync();
            return dbConnection;
        }
    }
}
