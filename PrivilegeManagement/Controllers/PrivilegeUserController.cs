using Microsoft.AspNetCore.Mvc;
using PrivilegeManagement.Dispatchs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivilegeManagement.Controllers
{
    [Route("api/privilege/[controller]/[action]")]
    [ApiController]
    public class PrivilegeUserController : PrivilegeController
    {
        private PrivilegeUserDispatch _privilegeUserDispatch;

        public PrivilegeUserController(PrivilegeUserDispatch privilegeUserDispatch)
        {
            _privilegeUserDispatch = privilegeUserDispatch;
        }


    }
}
