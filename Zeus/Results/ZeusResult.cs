using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Zeus.Results
{
    public class ZeusResult : ActionResult, IZeusResult
    {
        public object Data { get; set; }

        public ZeusResult(object data)
        {
            Data = data;
        }

        public ZeusResult(object data, JsonSerializerSettings jsonSerializerSettings)
        {
            Data = data;
            JsonSerializerSettings = jsonSerializerSettings ?? throw new ArgumentNullException(nameof(jsonSerializerSettings));
        }

        public string ContentType { get; set; }

        public JsonSerializerSettings JsonSerializerSettings { get; set; }

        public int? StatusCode { get; set; }


        public override Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            var services = context.HttpContext.RequestServices;
            var zeusResultFormatter = services.GetRequiredService<IZeusResultFormatter>();
            Data = zeusResultFormatter.Formatter(context, Data);
            var zeusResultExecutor = services.GetRequiredService<IZeusResultExecutor>();

            return zeusResultExecutor.ExecuteAsync(context, this);
        }
    }
}
