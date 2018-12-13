//**************************************
// Author              :    DannyShen
// Email               :    dannyshenl@163.com
// Create Time         :    2016/6/12 14:56:41
// Update Time         :    2016/6/12 14:56:41
// *************************************
// CLR Version         :    4.0.30319.42000
// Class Version       :    v1.0.0.0
// Class Description   :    
// *************************************
// Copyright ©SL 2016 . All rights reserved.
// *************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nereus {
    public class NReturn {
        /// <summary>
        /// 参数名
        /// </summary>
        public string Code {
            get;set;
        }

        /// <summary>
        /// 中文解释
        /// </summary>
        public string Name {
            get; set;
        }

        /// <summary>
        /// 返回类型
        /// </summary>
        public Type ReturnType {
            get; set;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class NReturnAttribute : Attribute {

        public NReturnAttribute(string name) {
            this.Name = name;
        }

        private NReturn _return = new NReturn();
        public string Name {
            get {
                return _return.Name;
            }

            set {
                _return.Name = value;
            }
        }
    }
}
