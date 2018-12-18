using Dapper;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PrivilegeManagement.Core
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
    }
}
