using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;

namespace Gaea
{
    public partial interface IGaeaPower
    {
        //Task<int> ExecuteNonQueryAsync( string commandText, object parameters, CommandType commandType, int? commandTimeout);

        //Task<int> ExecuteNonQueryAsync( string commandText, object parameters, IDbTransaction transcation, CommandType commandType, int? commandTimeout);

        Task<T> ExecuteScalarAsync<T>(string commandText, object parameters, CommandType commandType, int? commandTimeout);

        Task<int> ExecuteAsync(string commandText, object parameters, CommandType commandType, int? commandTimeout);

        Task<IEnumerable<T>> FindAllAsync<T>(GaeaSort sort,string sortField,int? commandTimeout) where T : IGaeaSon;

        Task<IEnumerable<T>> FindAsync<T>(string commandText, object parameters, int? commandTimeout) where T : IGaeaSon;

        Task<T> FindOneAsync<T>(string commandText, object parameters, int? commandTimeout) where T : IGaeaSon;

        Task<T> FindByIdAsync<T>(int id, int? commandTimeout) where T : IGaeaSon;

        Task<int> InsertAsync<T>(T son) where T : class, IGaeaSon;

        Task<int> UpdateAsync<T>(T son) where T : class, IGaeaSon;

        Task<int> BatchInsertAsync<T>(string commandText, IEnumerable<T> sons) where T : IGaeaSon;

        Task<int> BatchUpdateAsync<T>(string commandText, IEnumerable<T> sons) where T : IGaeaSon;

        Task<int> DeleteAsync<T>(T son) where T : class, IGaeaSon;

        Task<bool> DeleteAllAsync<T>() where T : class, IGaeaSon;
    }
}
