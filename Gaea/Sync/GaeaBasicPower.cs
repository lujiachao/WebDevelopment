using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

using Dapper;
using Dapper.Contrib.Extensions;
using System;

namespace Gaea
{
    public abstract partial class GaeaBasicPower : IGaeaPower
    {

        public virtual IDbConnection CreateConnection()
        {
            return connectionFactory.GetConnection();
        }

        public virtual TDbConnection CreateConnection<TDbConnection>() where TDbConnection : class, IDbConnection
        {
            return (connectionFactory.GetConnection()) as TDbConnection;
        }

        public virtual IEnumerable<T> Query<T>(string commandText, object parameters, CommandType commandType, int? commandTimeout = default(int?)) where T : class
        {
            using (var dbConnection = connectionFactory.GetConnection())
            {
                var result = dbConnection.Query<T>(commandText, parameters, null, commandTimeout: commandTimeout, commandType: commandType);
                dbConnection.Close();
                return result;
            }
        }

        public virtual T QuerySingle<T>(string commandText, object parameters, CommandType commandType, int? commandTimeout = default(int?)) where T : class
        {
            using (var dbConnection = connectionFactory.GetConnection())
            {
                var result = dbConnection.QuerySingleOrDefault<T>(commandText, parameters, null, commandTimeout: commandTimeout, commandType: commandType);
                dbConnection.Close();
                return result;
            }
        }

        public virtual T QuerySingle<T>(IDbConnection dbConnection, string commandText, object parameters, CommandType commandType, int? commandTimeout = default(int?)) where T : class
        {
            return dbConnection.QuerySingleOrDefault<T>(commandText, parameters, null, commandTimeout: commandTimeout, commandType: commandType);
        }

        public virtual T ExecuteScalar<T>(string commandText, object parameters, CommandType commandType, int? commandTimeout = default(int?))
        {
            using (var dbConnection = connectionFactory.GetConnection())
            {
                var result = ExecuteScalar<T>(dbConnection, commandText, parameters, commandType, commandTimeout);
                dbConnection.Close();
                return result;
            }
        }

        public virtual T ExecuteScalar<T>(IDbConnection dbConnection, string commandText, object parameters, CommandType commandType, int? commandTimeout = default(int?))
        {
            return dbConnection.ExecuteScalar<T>(commandText, parameters, null, commandTimeout, commandType);
        }

        public virtual int Execute(string commandText, object parameters, CommandType commandType, int? commandTimeout = default(int?))
        {
            using (var dbConnection = connectionFactory.GetConnection())
            {
                var result = Execute(dbConnection, commandText, parameters, commandType, commandTimeout);
                dbConnection.Close();
                return result;
            }
        }

        public virtual int Execute(IDbConnection dbConnection, string commandText, object parameters, CommandType commandType, int? commandTimeout = default(int?))
        {
            return dbConnection.Execute(commandText, parameters, null, commandTimeout, commandType);
        }

        public virtual IEnumerable<T> FindAll<T>(int? commandTimeout = default(int?)) where T : IGaeaSon
        {
            string commandText = $"SELECT * FROM {GaeaSon.CallName<T>()}";
            using (var dbConnection = connectionFactory.GetConnection())
            {
                var result = dbConnection.Query<T>(commandText, commandTimeout: commandTimeout);
                dbConnection.Close();
                return result;
            }
        }

        public virtual IEnumerable<T> FindAll<T>(GaeaSort sort = GaeaSort.ASC, string sortField = "id", int? commandTimeout = default(int?)) where T : IGaeaSon
        {
            string commandText = $"SELECT * FROM {GaeaSon.CallName<T>()}";
            if (sort == GaeaSort.ASC)
            {
                commandText = $"{commandText} ORDER BY {sortField}";
            }
            else
            {
                commandText = $"{commandText} ORDER BY {sortField} DESC";
            }
            using (var dbConnection = connectionFactory.GetConnection())
            {
                var result = dbConnection.Query<T>(commandText, null, null, commandTimeout: commandTimeout);
                dbConnection.Close();
                return result;
            }
        }

        public virtual IEnumerable<T> Find<T>(string commandText, object parameters, int? commandTimeout = default(int?)) where T : IGaeaSon
        {
            using (var dbConnection = connectionFactory.GetConnection())
            {
                var result = Find<T>(dbConnection, commandText, parameters, commandTimeout);
                dbConnection.Close();
                return result;
            }
        }

        public virtual IEnumerable<T> Find<T>(IDbConnection dbConnection, string commandText, object parameters, int? commandTimeout = default(int?)) where T : IGaeaSon
        {
            return dbConnection.Query<T>(commandText, parameters, null, commandTimeout: commandTimeout);
        }

        public virtual T FindOne<T>(string commandText, object parameters, int? commandTimeout = default(int?)) where T : IGaeaSon
        {
            using (var dbConnection = connectionFactory.GetConnection())
            {
                var result = FindOne<T>(dbConnection, commandText, parameters, commandTimeout);
                dbConnection.Close();
                return result;
            }
        }

        public virtual T FindOne<T>(IDbConnection dbConnection, string commandText, object parameters, int? commandTimeout = default(int?)) where T : IGaeaSon
        {
            return dbConnection.QuerySingleOrDefault<T>(commandText, parameters, null, commandTimeout);

        }

        public virtual T FindById<T>(int id, int? commandTimeout = default(int?)) where T : IGaeaSon
        {
            using (var dbConnection = connectionFactory.GetConnection())
            {
                var result = FindById<T>(dbConnection, id, commandTimeout);
                dbConnection.Close();
                return result;
            }
        }

        public virtual T FindById<T>(IDbConnection dbConnection, int id, int? commandTimeout = default(int?)) where T : IGaeaSon
        {
            string commandText = $"SELECT * FROM {GaeaSon.CallName<T>()} WHERE id=@id";
            return dbConnection.QuerySingleOrDefault<T>(commandText, new { id = id }, null, commandTimeout, CommandType.Text);
        }

        public virtual int Insert<T>(T son) where T : class, IGaeaSon
        {
            using (var dbConnection = connectionFactory.GetConnection())
            {
                var result = Insert(dbConnection, son);
                dbConnection.Close();
                return result;
            }
        }

        public virtual int Insert<T>(IDbConnection dbConnection, T son) where T : class, IGaeaSon
        {
            return (int)dbConnection.Insert(son);
        }

        public virtual int Update<T>(T son) where T : class, IGaeaSon
        {
            using (var dbConnection = connectionFactory.GetConnection())
            {
                var result = Update(dbConnection, son);
                dbConnection.Close();
                return result;
            }

        }

        public virtual int Update<T>(IDbConnection dbConnection, T son) where T : class, IGaeaSon
        {
            if (dbConnection.Update(son))
            {
                return 1;
            }
            return 0;
        }

        public virtual int BatchInsert<T>(string commandText, IEnumerable<T> sons) where T : IGaeaSon
        {
            using (var dbConnection = connectionFactory.GetConnection())
            {
                var result = BatchInsert(dbConnection, commandText, sons);
                dbConnection.Close();
                return result;
            }
        }


        public virtual int BatchInsert<T>(IDbConnection dbConnection, string commandText, IEnumerable<T> sons) where T : IGaeaSon
        {
            return dbConnection.Execute(commandText, sons);
        }

        public virtual int BatchUpdate<T>(string commandText, IEnumerable<T> sons) where T : IGaeaSon
        {
            using (var dbConnection = connectionFactory.GetConnection())
            {
                var result = dbConnection.Execute(commandText, sons);
                dbConnection.Close();
                return result;
            }
        }

        public virtual int Delete<T>(T son) where T : class, IGaeaSon
        {
            using (var dbConnection = connectionFactory.GetConnection())
            {
                var result = Delete(dbConnection, son);
                dbConnection.Close();
                return result;
            }
        }

        public virtual int Delete<T>(IDbConnection dbConnection, T son) where T : class, IGaeaSon
        {
            if (dbConnection.Delete(son, null))
            {
                return 1;
            }
            return 0;
        }

        public virtual bool DeleteAll<T>() where T : class, IGaeaSon
        {
            using (var dbConnection = connectionFactory.GetConnection())
            {
                var result = dbConnection.DeleteAll<T>();
                dbConnection.Close();
                return result;
            }
        }

        public virtual int Modify<T>(T son) where T : class, IGaeaSon
        {
            if (son.Id <= 0)
            {
                return Insert(son);
            }
            else
            {
                return Update(son);
            }
        }

        public virtual int Modify<T>(IDbConnection dbConnection, T son) where T : class, IGaeaSon
        {
            if (son.Id <= 0)
            {
                return Insert(dbConnection, son);
            }
            else
            {
                return Update(dbConnection, son);
            }
        }

        public virtual Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>> FindMultiple<TFirst, TSecond>(string commandText, object parameters) where TFirst : class
        {
            using (var dbConnection = connectionFactory.GetConnection())
            {
                var multipleResult = dbConnection.QueryMultiple(commandText, parameters);

                var result = new Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>>(
                     multipleResult.Read<TFirst>(), multipleResult.Read<TSecond>());

                dbConnection.Close();
                return result;
            }
        }
    }
}
