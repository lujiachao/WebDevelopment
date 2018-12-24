using Dapper;
using Dapper.Contrib.Extensions;
using MyDapper.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MyDapper.SqlPower
{
    /// <summary>
    /// 数据库访问基类 同步方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class BaseDAL<T> where T : class
    {
        private ConnectionFactory connectionFactory = new ConnectionFactory();
        /// <summary>
        /// 对象的表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 主键属性对象
        /// </summary>
        //public PropertyInfo PrimaryKey { get; set; }

        public BaseDAL()
        {
            this.TableName = EntityHelper.CallName<T>();
            //this.PrimaryKey = EntityHelper.GetSingleKey<T>();
        }

        ///<summary>
        ///获取表名
        /// </summary>
        public string GetTableName()
        {
            string tableName = EntityHelper.CallName<T>();
            return tableName;
        }
            
        /// <summary>
        /// 数据库连接
        /// </summary>
        public IDbConnection Connection
        {
            get
            {
                var connection = connectionFactory.CreateConnection();
                connection.Open();
                return connection;
            }
        }

        /// <summary>
        /// 返回数据库所有的对象集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.GetAll<T>();
            }
        }

        /// <summary>
        /// 查询数据库,返回指定ID的对象
        /// </summary>
        /// <param name="id">主键的值</param>
        /// <returns></returns>
        public T FindByID(object id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Get<T>(id);
            }
        }

        /// <summary>
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="info">指定的对象</param>
        /// <returns></returns>
        public bool Insert(T info)
        {
            bool result = false;
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Insert(info);
                result = true;
            }
            return result;
        }
        /// <summary>
        /// 插入指定对象集合到数据库中
        /// </summary>
        /// <param name="list">指定的对象集合</param>
        /// <returns></returns>
        public bool Insert(IEnumerable<T> list)
        {
            bool result = false;
            using (IDbConnection dbConnection = Connection)
            {
                result = dbConnection.Insert(list) > 0;
            }
            return result;
        }

        /// <summary>
        /// 更新对象属性到数据库中
        /// </summary>
        /// <param name="info">指定的对象</param>
        /// <returns></returns>
        public bool Update(T info)
        {
            bool result = false;
            using (IDbConnection dbConnection = Connection)
            {
                result = dbConnection.Update(info);
            }
            return result;
        }
        /// <summary>
        /// 更新指定对象集合到数据库中
        /// </summary>
        /// <param name="list">指定的对象集合</param>
        /// <returns></returns>
        public bool Update(IEnumerable<T> list)
        {
            bool result = false;
            using (IDbConnection dbConnection = Connection)
            {
                result = dbConnection.Update(list);
            }
            return result;
        }
        
        /// <summary>
        /// 从数据库中删除指定对象
        /// </summary>
        /// <param name="info">指定的对象</param>
        /// <returns></returns>
        public bool Delete(T info)
        {
            bool result = false;
            using (IDbConnection dbConnection = Connection)
            {
                result = dbConnection.Delete(info);
            }
            return result;
        }
        /// <summary>
        /// 从数据库中删除指定对象集合
        /// </summary>
        /// <param name="list">指定的对象集合</param>
        /// <returns></returns>
        public bool Delete(IEnumerable<T> list)
        {
            bool result = false;
            using (IDbConnection dbConnection = Connection)
            {
                result = dbConnection.Delete(list);
            }
            return result;
        }
        /// <summary>
        /// 根据指定对象的ID,从数据库中删除指定对象
        /// </summary>
        /// <param name="id">对象的ID</param>
        /// <returns></returns>
        public bool Delete(object id)
        {
            bool result = false;
            using (IDbConnection dbConnection = Connection)
            {
                string query = string.Format("DELETE FROM {0} WHERE id = @id", TableName);
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);

                result = dbConnection.Execute(query, parameters) > 0;
            }
            return result;
        }
        /// <summary>
        /// 从数据库中删除所有对象
        /// </summary>
        /// <returns></returns>
        public bool DeleteAll()
        {
            bool result = false;
            using (IDbConnection dbConnection = Connection)
            {
                result = dbConnection.DeleteAll<T>();
            }
            return result;
        }

        /// <summary>
        /// 执行SQL返回第一行第一列结果
        /// </summary>
        public virtual TT ExecuteScalar<TT>(string commandText, object parameters, CommandType commandType = CommandType.Text, int? commandTimeout = default(int?))
        {
            using (IDbConnection dbConnection = Connection)
            {
                var result = ExecuteScalar<TT>(dbConnection, commandText, parameters, commandType, commandTimeout);
                dbConnection.Close();
                return result;
            }
        }

        /// <summary>
        /// 执行SQL返回第一行第一列的结果 多用于事务
        /// </summary>
        public virtual TT ExecuteScalar<TT>(IDbConnection dbConnection, string commandText, object parameters, CommandType commandType = CommandType.Text, int? commandTimeout = default(int?))
        {
            return dbConnection.ExecuteScalar<TT>(commandText, parameters, null, commandTimeout, commandType);
        }

        /// <summary>
        /// 执行SQL返回影响行数
        /// </summary>
        public virtual int Execute(string commandText, object parameters, CommandType commandType = CommandType.Text, int? commandTimeout = default(int?))
        {
            using (var dbConnection = Connection)
            {
                var result = Execute(dbConnection, commandText, parameters, commandType, commandTimeout);
                dbConnection.Close();
                return result;
            }
        }

        ///<summary>
        ///执行SQL返回影响行数 多用于事务
        /// </summary>
        public virtual int Execute(IDbConnection dbConnection, string commandText, object parameters, CommandType commandType = CommandType.Text, int? commandTimeout = default(int?))
        {
            return dbConnection.Execute(commandText, parameters, null, commandTimeout, commandType);
        }

        /// <summary>
        /// 查询多行数据
        /// </summary>
        public virtual IEnumerable<TT> Query<TT>(string commandText, object parameters, CommandType commandType = CommandType.Text, int? commandTimeout = default(int?)) where TT : class
        {
            using (var dbConnection = Connection)
            {
                return Query<TT>(dbConnection, commandText, parameters, commandType, commandTimeout);
            }
        }


        /// <summary>
        /// 查询多行数据,多用于事务
        /// </summary>
        public virtual IEnumerable<TT> Query<TT>(IDbConnection dbConnection, string commandText, object parameters, CommandType commandType = CommandType.Text, int? commandTimeout = null) where TT : class
        {
            var result = dbConnection.Query<TT>(commandText, parameters, null, commandTimeout: commandTimeout, commandType: commandType);
            dbConnection.Close();
            return result;
        }

        ///<summary>
        /// 查询单条记录 (查询出多条会抛出异常，需要处理)
        /// </summary>
        public virtual TT QueryOne<TT>(string commandText, object parameters, CommandType commandType = CommandType.Text, int? commandTimeout = default(int?)) where TT : class
        {
            using (var dbConnection = Connection)
            {
                var result = dbConnection.QuerySingleOrDefault<TT>(commandText, parameters, null, commandTimeout, commandType);
                dbConnection.Close();
                return result;
            }
        }

        ///<summary>
        /// 查询单条记录 (查询出多条会抛出异常，需要处理)多用于数据库
        /// </summary>
        public virtual TT QueryOne<TT>(IDbConnection dbConnection, string commandText, object parameters, CommandType commandType = CommandType.Text, int? commandTimeout = default(int?)) where TT : class
        {
            var result = dbConnection.QuerySingleOrDefault<TT>(commandText, parameters, null, commandTimeout, commandType);
            return result;
        }

        /// <summary>
        /// 批量数据插入操作
        /// </summary>
        public virtual int BatchInsert<TT>(string commandText, IEnumerable<TT> sons) where TT : class
        {
            using (var dbConnection = Connection)
            {
                var result = BatchInsert<TT>(dbConnection, commandText, sons);
                dbConnection.Close();
                return result;
            }
        }

        /// <summary>
        /// 批量数据插入操作 多用于事务
        /// </summary>
        public virtual int BatchInsert<TT>(IDbConnection dbConnection, string commandText, IEnumerable<TT> sons) where TT : class
        {
            return dbConnection.Execute(commandText, sons);
        }

        /// <summary>
        /// 返回多组数据结果
        /// </summary>
        public virtual Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>> FindMultiple<TFirst, TSecond>(string commandText, object parameters) where TFirst : class
        {
            using (var dbConnection = Connection)
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
