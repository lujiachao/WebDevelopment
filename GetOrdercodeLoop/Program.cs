using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GetOrdercodeLoop
{
    class Program
    {
        public static void Main(string[] args)
        {
            getOrderCode();
        }

        public static async Task getOrderCode()
        {
            RePushResultDAL tenpayResultDAL = new RePushResultDAL();
            var lists = await tenpayResultDAL.GetTenPayResult();
            foreach (var list in lists)
            {
                var responseContext = list.ResponseContent;
                string regexStr = "(?<=(\"OrderCode\":\"))[.\\s\\S]*?(?=(\",\"Identity))";
                Regex regex = new Regex(regexStr, RegexOptions.IgnoreCase);
                string NameText = regex.Match(responseContext).Value;
                await tenpayResultDAL.UpdateTenPayResult(NameText,list.id);

            }
        }
    }
}
