using Dapper;
using MyDapper.SqlPower;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GetOrdercodeLoop
{
    public class RePushResultDAL : BaseDAL<TenpayResultLog>
    {
        public async Task<IEnumerable<TenpayResultLog>> GetTenPayResult()
        {
            string commandText = $"SELECT * FROM tenpay_result_log WHERE ResponseContent like '%SUCCESS%' and id <= 938";

            return await QueryAsync<TenpayResultLog>(commandText,null);
        }

        public async Task UpdateTenPayResult(string ordercode, int id)
        {
            string commandText = $"Update {EntityHelper.CallName<TenpayResultLog>()} set ordercode = @ordercode where id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            parameters.Add("@ordercode", ordercode);
            await ExecuteAsync(commandText, parameters);

            //await ExecuteAsync(commandText, new { ordercode, id});
        }
    }
}
