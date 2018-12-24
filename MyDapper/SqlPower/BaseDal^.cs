using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace MyDapper.SqlPower
{
    public partial class BaseDAL<T> where T : class
    {
        /// <summary>
        /// 返回数据库所有的对象集合
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            using (IDbConnection dbConnection = Connection)
            {
                return await dbConnection.GetAllAsync<T>();
            }
        }

        /// <summary>
        /// 查询数据库,返回指定ID的对象
        /// </summary>
        /// <param name="id">主键的值</param>
        /// <returns></returns>
        public virtual async Task<T> FindByIDAsync(object id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return await dbConnection.GetAsync<T>(id);
            }
        }
        /// <summary>
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="info">指定的对象</param>
        /// <returns></returns>
        public virtual async Task<bool> InsertAsync(T info)
        {
            bool result = false;
            using (IDbConnection dbConnection = Connection)
            {
                await dbConnection.InsertAsync(info);
                result = true;
            }
            return await Task<bool>.FromResult(result);
        }

        /// <summary>
        /// 插入指定对象集合到数据库中
        /// </summary>
        /// <param name="list">指定的对象集合</param>
        /// <returns></returns>
        public virtual async Task<bool> InsertAsync(IEnumerable<T> list)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return await dbConnection.InsertAsync(list) > 0;
            }
        }
        /// <summary>
        /// 更新对象属性到数据库中
        /// </summary>
        /// <param name="info">指定的对象</param>
        /// <returns></returns>
        public virtual async Task<bool> UpdateAsync(T info)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return await dbConnection.UpdateAsync(info);
            }
        }

        /// <summary>
        /// 更新指定对象集合到数据库中
        /// </summary>
        /// <param name="list">指定的对象集合</param>
        /// <returns></returns>
        public virtual async Task<bool> UpdateAsync(IEnumerable<T> list)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return await dbConnection.UpdateAsync(list);
            }
        }

        /// <summary>
        /// 从数据库中删除指定对象
        /// </summary>
        /// <param name="info">指定的对象</param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAsync(T info)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return await dbConnection.DeleteAsync(info);
            }
        }

        /// <summary>
        /// 从数据库中删除指定对象集合
        /// </summary>
        /// <param name="list">指定的对象集合</param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAsync(IEnumerable<T> list)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return await dbConnection.DeleteAsync(list);
            }
        }

        /// <summary>
        /// 根据指定对象的ID,从数据库中删除指定对象
        /// </summary>
        /// <param name="id">对象的ID</param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAsync(object id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = string.Format("DELETE FROM {0} WHERE id = @id", TableName);
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);

                return await dbConnection.ExecuteAsync(query, parameters) > 0;
            }
        }

        /// <summary>
        /// 从数据库中删除所有对象
        /// </summary>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAllAsync()
        {
            using (IDbConnection dbConnection = Connection)
            {
                return await dbConnection.DeleteAllAsync<T>();
            }
        }

        /// <summary>
        /// 执行SQL返回第一行第一列结果
        /// </summary>
        public virtual async Task<TT> ExecuteScalarAsync<TT>(string commandText, object parameters, CommandType commandType = CommandType.Text, int? commandTimeout = default(int?))
        {
            using (IDbConnection dbConnection = Connection)
            {
                var result = await ExecuteScalarAsync<TT>(dbConnection, commandText, parameters, commandType, commandTimeout);
                dbConnection.Close();
                return result;
            }
        }

        /// <summary>
        /// 执行SQL返回第一行第一列的结果 多用于事务
        /// </summary>
        public virtual async Task<TT> ExecuteScalarAsync<TT>(IDbConnection dbConnection, string commandText, object parameters, CommandType commandType = CommandType.Text, int? commandTimeout = default(int?))
        {
            return await dbConnection.ExecuteScalarAsync<TT>(commandText, parameters, null, commandTimeout, commandType);
        }

        /// <summary>
        /// 执行SQL返回影响行数
        /// </summary>
        public virtual async Task<int> ExecuteAsync(string commandText, object parameters, CommandType commandType = CommandType.Text, int? commandTimeout = default(int?))
        {
            using (var dbConnection = Connection)
            {
                var result = await ExecuteAsync(dbConnection, commandText, parameters, commandType, commandTimeout);
                dbConnection.Close();
                return result;
            }
        }

        ///<summary>
        ///执行SQL返回影响行数 多用于事务
        /// </summary>
        public virtual async Task<int> ExecuteAsync(IDbConnection dbConnection, string commandText, object parameters, CommandType commandType = CommandType.Text, int? commandTimeout = default(int?))
        {
            return await dbConnection.ExecuteAsync(commandText, parameters, null, commandTimeout, commandType);
        }

        /// <summary>
        /// 查询多行数据
        /// </summary>
        public virtual async Task<IEnumerable<TT>> QueryAsync<TT>(string commandText, object parameters, CommandType commandType = CommandType.Text, int? commandTimeout = default(int?)) where TT : class
        {
            using (var dbConnection = Connection)
            {
                return await QueryAsync<TT>(dbConnection, commandText, parameters, commandType, commandTimeout);
            }
        }


        /// <summary>
        /// 查询多行数据,多用于事务
        /// </summary>
        public virtual async Task<IEnumerable<TT>> QueryAsync<TT>(IDbConnection dbConnection, string commandText, object parameters, CommandType commandType = CommandType.Text, int? commandTimeout = null) where TT : class
        {
            var result = await dbConnection.QueryAsync<TT>(commandText, parameters, null, commandTimeout: commandTimeout, commandType: commandType);
            dbConnection.Close();
            return result;
        }

        ///<summary>
        /// 查询单条记录 (查询出多条会抛出异常，需要处理)
        /// </summary>
        public virtual async Task<TT> QueryOneAsync<TT>(string commandText, object parameters, CommandType commandType = CommandType.Text, int? commandTimeout = default(int?)) where TT : class
        {
            using (var dbConnection = Connection)
            {
                var result = await dbConnection.QuerySingleOrDefaultAsync<TT>(commandText, parameters, null, commandTimeout, commandType);
                dbConnection.Close();
                return result;
            }
        }

        ///<summary>
        /// 查询单条记录 (查询出多条会抛出异常，需要处理)多用于数据库
        /// </summary>
        public virtual async Task<TT> QueryOneAsync<TT>(IDbConnection dbConnection, string commandText, object parameters, CommandType commandType = CommandType.Text, int? commandTimeout = default(int?)) where TT : class
        {
            var result = await dbConnection.QuerySingleOrDefaultAsync<TT>(commandText, parameters, null, commandTimeout, commandType);
            return result;
        }

        /// <summary>
        /// 批量数据插入操作
        /// </summary>
        public virtual async Task<int> BatchInsertAsync<TT>(string commandText, IEnumerable<TT> sons) where TT : class
        {
            using (var dbConnection = Connection)
            {
                var result = await BatchInsertAsync<TT>(dbConnection, commandText, sons);
                dbConnection.Close();
                return result;
            }
        }

        /// <summary>
        /// 批量数据插入操作 多用于事务
        /// </summary>
        public virtual async Task<int> BatchInsertAsync<TT>(IDbConnection dbConnection, string commandText, IEnumerable<TT> sons) where TT : class
        {
            return await dbConnection.ExecuteAsync(commandText, sons);
        }

        /// <summary>
        /// 返回多组数据结果
        /// </summary>
        public virtual async Task<Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>>> FindMultipleAsync<TFirst, TSecond>(string commandText, object parameters) where TFirst : class
        {
            using (var dbConnection = Connection)
            {
                var multipleResult = await dbConnection.QueryMultipleAsync(commandText, parameters);

                var result = new Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>>(
                     multipleResult.Read<TFirst>(), multipleResult.Read<TSecond>());

                dbConnection.Close();
                return result;
            }
        }
    }
}
