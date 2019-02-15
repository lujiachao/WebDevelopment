﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivilegeManagement.Results
{
    public class PageList<T> : PrivilegeBaseResult 
    {
        /// <summary>
        /// 返回code
        /// </summary>
        public int code
        {
            get; set;
        }

        /// <summary>
        /// 数据传输消息
        /// </summary>
        public string msg
        {
            get; set;
        }

        ///<summary>
        /// 记录数
        /// </summary>
        public int count
        {
            get; set;
        }

        public List<T> data
        {
            get; set;
        }
    }
}
