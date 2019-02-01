using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PrivilegeManagement.Common.Enum;
using PrivilegeManagement.Results;
using System;

namespace PrivilegeManagement.Controllers
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    public class PrivilegeController : ControllerBase
    {
        public const string DefaultDateStringFormat = "yyyy-MM-dd";

        public const string DefaultTimeStringFormat = "yyyy-MM-dd HH:mm:ss";

        //Nonaction 这并不是一个控制器，防止框架认错
        [NonAction]
        public PrivilegeBaseResult Successed(object data, string message = "正常请求")
        {
            var resultDataSuccess = new ResultDataSuccess
            {
                GuidRequest = Guid.NewGuid().ToString(),
                Code = (int)ResultStatusCode.Success,
                Message = message,
                Data = data
            };
            return resultDataSuccess;
        }

        [NonAction]
        public PrivilegeBaseResult Failed(object data, string message = "失败请求")
        {
            var resultDataFailed = new ResultDataFaild
            {
                GuidRequest = Guid.NewGuid().ToString(),
                Code = (int)ResultStatusCode.UnknowError,
                Message = message,
                Data = data
            };
            return resultDataFailed;
        }

        [NonAction]
        public PrivilegeBaseResult Exceptioned(object data, int code, string message = "异常请求")
        {
            var resultDataException = new ResultDataException
            {
                GuidRequest = Guid.NewGuid().ToString(),
                Code = code,
                Message = message,
                Data = data
            };
            return resultDataException;
        }
    }
}
