using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivilegeManagement.Results
{
    /// <summary>
    /// 异常信息返回结果封装
    /// </summary>
    public class ResultDataException : PrivilegeBaseResult
    {
        //重构方法
        public ResultDataException()
        {

        }
        //异常guid
        public string GuidRequest { get; set; }
        //异常编码
        public int Code { get; set; }
        //异常信息
        public string Message { get; set; }
        //异常数据
        public object Data { get; set; }
    }
}
