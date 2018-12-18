using DapperForDotnet.Dal;
using DapperForDotnet.DAL;
using DapperForDotnet.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DapperForDotnet.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
       // GET api/values
       [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [HttpGet]
        public ActionResult<string> GetTableName()
        {
            string TableName = EntityHelper.CallName<CustomerInfo>();
            return TableName;
        }

        //POST api/values
       [HttpPost]
        public void Post([FromBody] string value)
        {
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

        [HttpGet]
        public ActionResult<string> GetAll()
        {
            CustomerDAL customer = new CustomerDAL();
            var list = customer.GetAll();
            return list.ToString();
        }
    }
}
