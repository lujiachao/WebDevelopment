using PrivilegeManagement.Common.Enum;
using PrivilegeManagement.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivilegeManagement.Dispatchs
{
    public class BaseDispatch
    {
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
