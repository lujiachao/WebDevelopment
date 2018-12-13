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
        protected DefaultGaeaConnectionFactory connectionFactory;

        public GaeaBasicPower()
        {
            connectionFactory = new DefaultGaeaConnectionFactory();
        }

        public virtual async Task<IDbConnection> CreateConnectionAsync()
        {
            return await connectionFactory.GetConnectionAsync();
        }

        public virtual async Task<TDbConnection> CreateConnectionAsync<TDbConnection>() where TDbConnection : class, IDbConnection
        {
            return (await connectionFactory.GetConnectionAsync()) as TDbConnection;
        }

        public virtual async Task<IEnumerable<T>> QueryAsync<T>(string commandText, object parameters, CommandType commandType, int? commandTimeout = default(int?)) where T : class
        {
            using (var dbConnection = await connectionFactory.GetConnectionAsync())
            {
                var result = await dbConnection.QueryAsync<T>(commandText, parameters, null, commandTimeout, commandType);
                dbConnection.Close();
                return result;
            }
        }

        public virtual async Task<T> QuerySingleAsync<T>(string commandText, object parameters, CommandType commandType, int? commandTimeout = default(int?)) where T : class
        {
            using (var dbConnection = await connectionFactory.GetConnectionAsync())
            {
                var result = await dbConnection.QuerySingleOrDefaultAsync<T>(commandText, parameters, null, commandTimeout, commandType);
                dbConnection.Close();
                return result;
            }
        }

        public virtual async Task<T> QuerySingleAsync<T>(IDbConnection dbConnection, string commandText, object parameters, CommandType commandType, int? commandTimeout = default(int?)) where T : class
        {
            var result = await dbConnection.QuerySingleOrDefaultAsync<T>(commandText, parameters, null, commandTimeout, commandType);
            return result;

        }

        public virtual async Task<T> ExecuteScalarAsync<T>(string commandText, object parameters, CommandType commandType, int? commandTimeout = default(int?))
        {
            using (var dbConnection = await connectionFactory.GetConnectionAsync())
            {
                var result = await ExecuteScalarAsync<T>(dbConnection, commandText, parameters, commandType, commandTimeout);
                dbConnection.Close();
                return result;
            }
        }

        public virtual async Task<T> ExecuteScalarAsync<T>(IDbConnection dbConnection, string commandText, object parameters, CommandType commandType, int? commandTimeout = default(int?))
        {
            return await dbConnection.ExecuteScalarAsync<T>(commandText, parameters, null, commandTimeout, commandType);
        }

        public virtual async Task<int> ExecuteAsync(string commandText, object parameters, CommandType commandType, int? commandTimeout = default(int?))
        {
            using (var dbConnection = await connectionFactory.GetConnectionAsync())
            {
                var result = await ExecuteAsync(dbConnection, commandText, parameters, commandType, commandTimeout);
                dbConnection.Close();
                return result;
            }
        }

        public virtual async Task<int> ExecuteAsync(IDbConnection dbConnection, string commandText, object parameters, CommandType commandType, int? commandTimeout = default(int?))
        {
            return await dbConnection.ExecuteAsync(commandText, parameters, null, commandTimeout, commandType);
        }

        public virtual async Task<IEnumerable<T>> FindAllAsync<T>() where T : IGaeaSon
        {
            string commandText = $"SELECT * FROM {GaeaSon.CallName<T>()}";
            using (var dbConnection = await connectionFactory.GetConnectionAsync())
            {
                var result = await dbConnection.QueryAsync<T>(commandText);
                dbConnection.Close();
                return result;
            }
        }

        public virtual async Task<IEnumerable<T>> FindAllAsync<T>(GaeaSort sort = GaeaSort.ASC, string sortField = "id", int? commandTimeout = default(int?)) where T : IGaeaSon
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
            using (var dbConnection = await connectionFactory.GetConnectionAsync())
            {
                var result = await dbConnection.QueryAsync<T>(commandText, null, null, commandTimeout, CommandType.Text);
                dbConnection.Close();
                return result;
            }
        }

        public virtual async Task<IEnumerable<T>> FindAsync<T>(string commandText, object parameters, int? commandTimeout = default(int?)) where T : IGaeaSon
        {
            using (var dbConnection = await connectionFactory.GetConnectionAsync())
            {
                var result = await FindAsync<T>(dbConnection, commandText, parameters, commandTimeout);
                dbConnection.Close();
                return result;
            }
        }

        public virtual async Task<IEnumerable<T>> FindAsync<T>(IDbConnection dbConnection, string commandText, object parameters, int? commandTimeout = default(int?)) where T : IGaeaSon
        {
            return await dbConnection.QueryAsync<T>(commandText, parameters, null, commandTimeout);
        }

        public virtual async Task<T> FindOneAsync<T>(string commandText, object parameters, int? commandTimeout = default(int?)) where T : IGaeaSon
        {
            using (var dbConnection = await connectionFactory.GetConnectionAsync())
            {
                var result = await FindOneAsync<T>(dbConnection, commandText, parameters, commandTimeout);
                dbConnection.Close();
                return result;
            }
        }

        public virtual async Task<T> FindOneAsync<T>(IDbConnection dbConnection, string commandText, object parameters, int? commandTimeout = default(int?)) where T : IGaeaSon
        {
            return await dbConnection.QuerySingleOrDefaultAsync<T>(commandText, parameters, null, commandTimeout);
        }

        public virtual async Task<T> FindByIdAsync<T>(int id, int? commandTimeout = default(int?)) where T : IGaeaSon
        {
            using (var dbConnection = await connectionFactory.GetConnectionAsync())
            {
                var result = await FindByIdAsync<T>(dbConnection, id, commandTimeout);
                dbConnection.Close();
                return result;
            }
        }

        public virtual async Task<T> FindByIdAsync<T>(IDbConnection dbConnection, int id, int? commandTimeout = default(int?)) where T : IGaeaSon
        {
            string commandText = $"SELECT * FROM {GaeaSon.CallName<T>()} WHERE id=@id";
            return await dbConnection.QuerySingleOrDefaultAsync<T>(commandText, new { id = id }, null, commandTimeout, CommandType.Text);
        }

        public virtual async Task<int> InsertAsync<T>(T son) where T : class, IGaeaSon
        {
            using (var dbConnection = await connectionFactory.GetConnectionAsync())
            {
                var result = await InsertAsync(dbConnection, son);
                dbConnection.Close();
                return result;
            }
        }

        public virtual async Task<int> InsertAsync<T>(IDbConnection dbConnection, T son) where T : class, IGaeaSon
        {
            return await dbConnection.InsertAsync(son);
        }

        public virtual async Task<int> UpdateAsync<T>(T son) where T : class, IGaeaSon
        {
            using (var dbConnection = await connectionFactory.GetConnectionAsync())
            {
                var result = await UpdateAsync(dbConnection, son);
                dbConnection.Close();
                return result;
            }

        }

        public virtual async Task<int> UpdateAsync<T>(IDbConnection dbConnection, T son) where T : class, IGaeaSon
        {
            if (await dbConnection.UpdateAsync(son))
            {
                return 1;
            }
            return 0;
        }

        public virtual async Task<int> BatchInsertAsync<T>(string commandText, IEnumerable<T> sons) where T : IGaeaSon
        {
            using (var dbConnection = await connectionFactory.GetConnectionAsync())
            {
                var result = await BatchInsertAsync(dbConnection, commandText, sons);
                dbConnection.Close();
                return result;
            }
        }


        public virtual async Task<int> BatchInsertAsync<T>(IDbConnection dbConnection, string commandText, IEnumerable<T> sons) where T : IGaeaSon
        {
            return await dbConnection.ExecuteAsync(commandText, sons);
        }

        public virtual async Task<int> BatchUpdateAsync<T>(string commandText, IEnumerable<T> sons) where T : IGaeaSon
        {
            using (var dbConnection = await connectionFactory.GetConnectionAsync())
            {
                var result = await dbConnection.ExecuteAsync(commandText, sons);
                dbConnection.Close();
                return result;
            }
        }

        public virtual async Task<int> DeleteAsync<T>(T son) where T : class, IGaeaSon
        {
            using (var dbConnection = await connectionFactory.GetConnectionAsync())
            {
                var result = await DeleteAsync(dbConnection, son);
                dbConnection.Close();
                return result;
            }
        }

        public virtual async Task<int> DeleteAsync<T>(IDbConnection dbConnection, T son) where T : class, IGaeaSon
        {
            if (await dbConnection.DeleteAsync(son, null))
            {
                return 1;
            }
            return 0;
        }

        public virtual async Task<bool> DeleteAllAsync<T>() where T : class, IGaeaSon
        {
            using (var dbConnection = await connectionFactory.GetConnectionAsync())
            {
                var result = await dbConnection.DeleteAllAsync<T>();
                dbConnection.Close();
                return result;
            }
        }

        public virtual async Task<int> ModifyAsync<T>(T son) where T : class, IGaeaSon
        {
            if (son.Id <= 0)
            {
                return await InsertAsync(son);
            }
            else
            {
                return await UpdateAsync(son);
            }
        }

        public virtual async Task<int> ModifyAsync<T>(IDbConnection dbConnection, T son) where T : class, IGaeaSon
        {
            if (son.Id <= 0)
            {
                return await InsertAsync(dbConnection, son);
            }
            else
            {
                return await UpdateAsync(dbConnection, son);
            }
        }

        public virtual async Task<Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>>> FindMultipleAsync<TFirst, TSecond>(string commandText, object parameters) where TFirst : class
        {
            using (var dbConnection = await connectionFactory.GetConnectionAsync())
            {
                var multipleResult = await dbConnection.QueryMultipleAsync(commandText, parameters);

                var result = new Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>>(
                    await multipleResult.ReadAsync<TFirst>(), await multipleResult.ReadAsync<TSecond>());

                dbConnection.Close();
                return result;
            }
        }
    }
}
