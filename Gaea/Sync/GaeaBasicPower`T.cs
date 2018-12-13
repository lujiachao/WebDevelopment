using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

using Dapper;
namespace Gaea
{
    public partial class GaeaBasicPower<T> : GaeaBasicPower where T : class, IGaeaSon
    {

        public virtual T ExecuteScalar(string commandText, object parameters, CommandType commandType, int? commandTimeout = default(int?))
        {
            return base.ExecuteScalar<T>(commandText, parameters, commandType, commandTimeout);
        }

        public virtual T ExecuteScalar(IDbConnection dbConnection, string commandText, object parameters, CommandType commandType, int? commandTimeout = default(int?))
        {
            return dbConnection.ExecuteScalar<T>(commandText, parameters, null, commandTimeout, commandType);
        }

        public virtual IEnumerable<T> FindAll(int? commandTimeout = default(int?))
        {
            return base.FindAll<T>(commandTimeout: commandTimeout);
        }

        public virtual IEnumerable<T> FindAll(GaeaSort gaeaSort)
        {
            return base.FindAll<T>(sort: gaeaSort);
        }

        public IEnumerable<T> Find(string commandText, object parameters, int? commandTimeout = default(int?))
        {
            return base.Find<T>(commandText, parameters, commandTimeout);
        }

        public virtual T FindOne(string commandText, object parameters, int? commandTimeout = default(int?))
        {
            return base.FindOne<T>(commandText, parameters);
        }

        public virtual T FindOne(IDbConnection dbConnection, string commandText, object parameters, int? commandTimeout = default(int?))
        {
            return base.FindOne<T>(dbConnection, commandText, parameters);
        }

        public virtual T FindById(int id, int? commandTimeout = default(int?))
        {
            return base.FindById<T>(id, commandTimeout);
        }

        public virtual T FindById(IDbConnection dbConnection, int id, int? commandTimeout = default(int?))
        {
            return base.FindById<T>(dbConnection, id, commandTimeout);
        }

        public virtual int Insert(T son)
        {
            return base.Insert(son);
        }

        public virtual int Insert(IDbConnection dbConnection, T son)
        {
            return base.Insert(dbConnection, son);
        }

        public virtual int Update(T son)
        {
            return base.Update(son);
        }

        public virtual int Update(IDbConnection dbConnection, T son)
        {
            return base.Update(dbConnection, son);
        }


        public virtual int BatchInsert(string commandText, IEnumerable<T> sons)
        {
            return base.BatchInsert(commandText, sons);
        }

        public virtual int BatchInsert(IDbConnection dbConnection, string commandText, IEnumerable<T> sons)
        {
            return base.BatchInsert(dbConnection, commandText, sons);
        }

        public virtual int Delete(T son)
        {
            return base.Delete(son);
        }
        public virtual int Delete(IDbConnection dbConnection, T son)
        {
            return base.Delete(dbConnection, son);
        }
        public virtual bool DeleteAll()
        {
            return base.DeleteAll<T>();
        }

        public virtual int Modify(T son)
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

        public virtual int Modify(IDbConnection dbConnection, T son)
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


    }
}
