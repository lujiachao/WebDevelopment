using DapperForDotnet.Dal;
using DapperForDotnet.DAL;
using DapperForDotnet.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public IEnumerable<CustomerInfo> GetAll()
        {
            CustomerDAL customer = new CustomerDAL();
            var list = customer.GetAll();
            return list;
        }

        [HttpGet]
        public async Task<bool> InsertOne()
        {
            CustomerDAL customer = new CustomerDAL();
            var insertInfo = new CustomerInfo() { Name = "test", Age = 30, Creator = "test" };
            var insertList = new List<CustomerInfo>() { insertInfo };
            var flag = customer.Insert(insertList);
            return flag;
        }
    }
}
