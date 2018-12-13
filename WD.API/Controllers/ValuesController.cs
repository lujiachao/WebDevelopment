using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WD.API.Argument;
using WD.Common.WDEnum;
using WD.Common.WDException;

namespace WD.API.Controllers
{
    [Produces("application/json")]
    [EnableCors("AllowCors")]
    [Route("api/[controller]/[action]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost]
        public async Task Test()
        {
            if (1 != 2)
            {
                throw new TExSException(EnumErrorStatus.APIArgumentError, "REGISTER FAILD!ARGUADMINREGISTER IS NULL");
            }
        }

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
