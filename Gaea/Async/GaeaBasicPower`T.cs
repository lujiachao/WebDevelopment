using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

using Dapper;
using Dapper.Contrib.Extensions;
namespace Gaea
{
    public partial class GaeaBasicPower<T> : GaeaBasicPower where T : class, IGaeaSon
    {
        public string GaeaName
        {
            get
            {
                return GaeaSon.CallName<T>();
            }
        }

        public virtual async Task<T> ExecuteScalarAsync(string commandText, object parameters, CommandType commandType, int? commandTimeout = default(int?))
        {
            return await base.ExecuteScalarAsync<T>(commandText, parameters, commandType, commandTimeout);
        }

        public virtual async Task<T> ExecuteScalarAsync(IDbConnection dbConnection, string commandText, object parameters, CommandType commandType, int? commandTimeout = default(int?))
        {
            return await dbConnection.ExecuteScalarAsync<T>(commandText, parameters, null, commandTimeout, commandType);
        }

        public virtual async Task<IEnumerable<T>> FindAllAsync(int? commandTimeout = default(int?))
        {
            return await base.FindAllAsync<T>(commandTimeout: commandTimeout);
        }

        public virtual async Task<IEnumerable<T>> FindAllAsync(GaeaSort gaeaSort)
        {
            return await base.FindAllAsync<T>(sort: gaeaSort);
        }

        public async Task<IEnumerable<T>> FindAsync(string commandText, object parameters, int? commandTimeout = default(int?))
        {
            return await base.FindAsync<T>(commandText, parameters, commandTimeout);
        }



        public virtual async Task<T> FindOneAsync(string commandText, object parameters, int? commandTimeout = default(int?))
        {
            return await base.FindOneAsync<T>(commandText, parameters);
        }

        public virtual async Task<T> FindOneAsync(IDbConnection dbConnection, string commandText, object parameters, int? commandTimeout = default(int?))
        {
            return await base.FindOneAsync<T>(dbConnection, commandText, parameters);
        }


        public virtual async Task<T> FindByIdAsync(int id, int? commandTimeout = default(int?))
        {
            return await base.FindByIdAsync<T>(id, commandTimeout);
        }

        public virtual async Task<T> FindByIdAsync(IDbConnection dbConnection, int id, int? commandTimeout = default(int?))
        {
            return await base.FindByIdAsync<T>(dbConnection, id, commandTimeout);
        }

        public virtual async Task<int> InsertAsync(T son)
        {
            return await base.InsertAsync(son);
        }

        public virtual async Task<int> InsertAsync(IDbConnection dbConnection, T son)
        {
            return await base.InsertAsync(dbConnection, son);
        }

        public virtual async Task<int> UpdateAsync(T son)
        {
            return await base.UpdateAsync(son);
        }

        public virtual async Task<int> UpdateAsync(IDbConnection dbConnection, T son)
        {
            return await base.UpdateAsync(dbConnection, son);
        }

        public virtual async Task<int> BatchInsertAsync(string commandText, IEnumerable<T> sons)
        {
            return await base.BatchInsertAsync(commandText, sons);
        }

        public virtual async Task<int> BatchInsertAsync(IDbConnection dbConnection, string commandText, IEnumerable<T> sons)
        {
            return await base.BatchInsertAsync(dbConnection, commandText, sons);
        }

        public virtual async Task<int> DeleteAsync(T son)
        {
            return await base.DeleteAsync(son);
        }
        public virtual async Task<int> DeleteAsync(IDbConnection dbConnection, T son)
        {
            return await base.DeleteAsync(dbConnection, son);
        }
        public virtual async Task<bool> DeleteAllAsync()
        {
            return await base.DeleteAllAsync<T>();
        }

        public virtual async Task<int> ModifyAsync(T son)
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

        public virtual async Task<int> ModifyAsync(IDbConnection dbConnection, T son)
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


    }
}
