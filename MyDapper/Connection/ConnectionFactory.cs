using MyDapper.Connection.MyEnum;
using MyDapper.JsonHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MyDapper.Connection
{
    public class ConnectionFactory : IConnectionFactory
    {
        //获取配置文件信息
        //public static IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
        //public static IConfigurationRoot configuration = builder.Build();

        ///<summary>
        /// 转换数据库类型
        /// </summary>
        /// <param name="databaseType">数据库类型</param>
        /// <returns>DatabaseType</returns>
        public DatabaseType GetDataBaseType(string databaseType)
        {
            DatabaseType returnValue = DatabaseType.MySql;
            foreach (DatabaseType dbType in Enum.GetValues(typeof(DatabaseType)))
            {
                if (dbType.ToString().Equals(databaseType, StringComparison.OrdinalIgnoreCase))
                {
                    returnValue = dbType;
                    break;
                }
            }
            return returnValue;
        }

        ///<summary>
        ///获取数据库连接
        ///</summary>
        ///<return>IDbConnection</return> 
        public IDbConnection CreateConnection()
        {
            IDbConnection connection = null;

            //获取配置进行转换
            var type = JsonConfigurationHelper.GetAppSettingSingle("ComponentDbType");
            var dbType = GetDataBaseType(type);

            //DefaultDatabase 根据这个配置项获取对应连接字符串
            var database = JsonConfigurationHelper.GetAppSettingSingle("DefaultDatabase");
            if (string.IsNullOrEmpty(database))
            {
                database = "mysql";//默认配置
            }
            //var jsonConfigurationHelper = JsonConfigurationHelper.GetAppSettings<ConnectionStrings>("ConnectionStrings");
            var strConn = JsonConfigurationHelper.GetAppSettingSingle("ConnectionStrings:" + database);
            switch (dbType)
            {
                case DatabaseType.SqlServer:
                    connection = new System.Data.SqlClient.SqlConnection(strConn);
                    break;
                case DatabaseType.MySql:
                    connection = new MySql.Data.MySqlClient.MySqlConnection(strConn);
                    break;
                case DatabaseType.Oracle:
                    connection = new Oracle.ManagedDataAccess.Client.OracleConnection(strConn);
                    //  connection = new System.Data.OracleClient.OracleConnection(strConn);
                    break;
                default:
                    connection = null;
                    break;

            }

            return connection;
        }
    }
}
