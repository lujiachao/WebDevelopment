using System.Collections.Generic;
using System.Data;

namespace Gaea
{
    public partial interface IGaeaPower
    {
        //int ExecuteNonQuery( string commandText, object parameters, CommandType commandType, int? commandTimeout);

        //int ExecuteNonQuery( string commandText, object parameters, IDbTransaction transcation, CommandType commandType, int? commandTimeout);

        T ExecuteScalar<T>(string commandText, object parameters, CommandType commandType, int? commandTimeout);

        int Execute(string commandText, object parameters, CommandType commandType, int? commandTimeout);

        IEnumerable<T> FindAll<T>(int? commandTimeout) where T : IGaeaSon;

        IEnumerable<T> Find<T>(string commandText, object parameters, int? commandTimeout) where T : IGaeaSon;

        T FindOne<T>(string commandText, object parameters, int? commandTimeout) where T : IGaeaSon;

        T FindById<T>(int id, int? commandTimeout) where T : IGaeaSon;

        int Insert<T>(T son) where T : class, IGaeaSon;

        int Update<T>(T son) where T : class, IGaeaSon;

        int BatchInsert<T>(string commandText, IEnumerable<T> sons) where T : IGaeaSon;

        int BatchUpdate<T>(string commandText, IEnumerable<T> sons) where T : IGaeaSon;

        int Delete<T>(T son) where T : class, IGaeaSon;

        bool DeleteAll<T>() where T : class, IGaeaSon;
        
    }
}
