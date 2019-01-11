using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DapperForDotnet.Dal;
using Microsoft.AspNetCore.Mvc;
using PrivilegeManagement.Common.Enum;
using PrivilegeManagement.MiddleWare;
using PrivilegeManagement.Models;

namespace PrivilegeManagement.Controllers
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

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] string value)
        {
            throw new PrivilegeException(400,"测试异常");
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
        public async Task<ActionResult> InsertOne()
        {
            throw new PrivilegeException(EnumPrivilegeException.测试异常, "测试异常");
            CustomerDAL customer = new CustomerDAL();
            var insertInfo = new CustomerInfo() { Name = "test", Age = 30, Creator = "test" };
            var insertList = new List<CustomerInfo>() { insertInfo };
            var flag = customer.Insert(insertList);
            //return flag;
        }

        //[HttpGet]

        //public void SelectObject()
        //{
        //    CustomerDAL customer = new CustomerDAL();
        //    string commandSQL = @"SELECT * FROM T_CUSTOMER WHERE ID = 2";
        //    CustomerInfo customerInfo = customer.SelectString<CustomerInfo>(commandSQL);
        //    int A = 1;
        //}
    }
}
