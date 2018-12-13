using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WD.API.Argument;
using WD.Common.WDEnum;
using WD.Common.WDException;

namespace WD.API.Controllers
{
    [Produces("application/json")]
    [EnableCors("AllowCors")]
    [Route("api/[controller]/[action]")]
    public class FirstController : Controller
    {
        [HttpPost]
        public async Task<ArguTest> ArguTest([FromBody]ArguTest arguTest)
        {
            if (arguTest == null)
            {
                throw new TExSException(EnumErrorStatus.APIArgumentError, "REGISTER FAILD!ARGUADMINREGISTER IS NULL");
            }
            return arguTest;

        }
    }
}
