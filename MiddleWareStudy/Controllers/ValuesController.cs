using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiddleWareStudy.Argu;
using MiddleWareStudy.Middleware;
using Newtonsoft.Json;

namespace MiddleWareStudy.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            throw new PrivilegeException(300, "测试异常");
            return "dasdasdasd";
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public ActionResult<string> Post([FromBody] string value)
        {
            return value;
        }

        [HttpPost]
        public ActionResult<string> PostTest([FromBody] ArguLogin arguLogin)
        {
            if (arguLogin == null)
            {
                throw new PrivilegeException(001, "Argu is null!Argu is not null");
            }
            return JsonConvert.SerializeObject(arguLogin);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
