﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivilegeManagement.Common.Enum
{
    public enum EnumPrivilegeException
    {
        测试异常 = 300,
        入参为空 = 1000,
        请求上下文为空 = 1001,
        未查询到该身份 = 1002,
        注册用户名为空 = 1100,
        注册密码不能为空 = 1101,
        注册昵称不可为空 = 1102,
        手机号不可为空 = 1103,
        用户名已存在 = 1104,
        该手机号已存在 = 1105
    }
}
