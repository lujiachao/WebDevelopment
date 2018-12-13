using System.Data.Common;

using Autofac;
using MySql.Data.MySqlClient;
namespace Gaea.MySql
{
    public class GaeaMySqlPower:GaeaBasicPower
    {
        public static void Register(string connectionString = "", string connectionStringConfigureName = "mysql")
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<MySqlConnection>().As<DbConnection>();
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                DefaultGaeaConnectionFactory.RegisterByConfiguration(builder.Build(), connectionStringConfigureName, "``");
            }
            else {
                DefaultGaeaConnectionFactory.Register(builder.Build(), connectionString, "``");
            }
           
        }
    }
}
