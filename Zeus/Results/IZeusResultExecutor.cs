using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Zeus.Results
{
    public interface IZeusResultExecutor
    {
        Task ExecuteAsync(ActionContext context, ZeusResult result);
    }
}
