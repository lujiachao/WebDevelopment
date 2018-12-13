using System.Threading.Tasks;
using System.Data.Common;

namespace Gaea
{
    public interface IGaeaConnectionFactory
    {
        DbConnection GetConnection();

        Task<DbConnection> GetConnectionAsync();
    }
}
