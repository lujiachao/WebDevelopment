//**************************************
// Author              :    DannyShen
// Email               :    dannyshenl@163.com
// Create Time         :    2016/6/12 14:42:43
// Update Time         :    2016/6/12 14:42:43
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
    public class NAction {

        public NAction() {
            this.Returns = new List<NReturn>();
            this.Params = new List<NParam>();
        }

        /// <summary>
        /// 请求类型
        /// </summary>
        public string Method {
            get; set;
        }

        /// <summary>
        /// 接口Code
        /// </summary>
        public string Code {
            get; set;
        }

        /// <summary>
        /// 接口名称
        /// </summary>
        public string Name {
            get; set;
        }

        /// <summary>
        /// 接口Route
        /// </summary>
        public string Route {
            get; set;
        }

        /// <summary>
        /// 接口描述
        /// </summary>
        public string Description {
            get; set;
        }

        /// <summary>
        /// 是否显示描述
        /// </summary>
        public bool Enable {
            get; set;
        }

        /// <summary>
        /// 参数描述
        /// </summary>
        public string[] ParamExplain {
            get; set;
        }

        /// <summary>
        /// 参数类型
        /// </summary>
        public Type ParamType {
            get; set;
        }

        /// <summary>
        /// 返回描述
        /// </summary>
        public string ReturnExplain {
            get; set;
        }

        /// <summary>
        /// 返回类型
        /// </summary>
        public Type ReturnType {
            get; set;
        }

        /// <summary>
        /// 参数
        /// </summary>
        public List<NParam> Params {
            get; set;
        }

        /// <summary>
        /// 返回值
        /// </summary>
        public List<NReturn> Returns {
            get; set;
        }
    }

    public class NActionAttribute : Attribute {
        private NAction _action = new NAction();

        /// <summary>
        /// 请求类型
        /// </summary>
        public string Method {
            get {
                return _action.Method;
            }
            set {
                _action.Method = value;
            }
        }

        /// <summary>
        /// 接口Code
        /// </summary>
        public string Code {
            get {
                return _action.Code;
            }
            set {
                _action.Code = value;
            }
        }

        /// <summary>
        /// 接口名称
        /// </summary>
        public string Name {
            get {
                return _action.Name;
            }
            set {
                _action.Name = value;
            }
        }

        /// <summary>
        /// 接口Route
        /// </summary>
        public string Route {
            get {
                return _action.Route;
            }
            set {
                _action.Route = value;
            }
        }

        /// <summary>
        /// 接口描述
        /// </summary>
        public string Description {
            get {
                return _action.Description;
            }
            set {
                _action.Description = value;
            }
        }

        /// <summary>
        /// 是否显示描述
        /// </summary>
        public bool Enable {
            get {
                return _action.Enable;
            }
            set {
                _action.Enable = value;
            }
        }

        /// <summary>
        /// 参数描述
        /// </summary>
        public string[] ParamExplain {
            get {
                return _action.ParamExplain;
            }
            set {
                _action.ParamExplain = value;
            }
        }

        /// <summary>
        /// 参数类型
        /// </summary>
        public Type ParamType {
            get {
                return _action.ParamType;
            }
            set {
                _action.ParamType = value;
            }
        }

        /// <summary>
        /// 返回描述
        /// </summary>
        public string ReturnExplain {
            get {
                return _action.ReturnExplain;
            }
            set {
                _action.ReturnExplain = value;
            }
        }

        /// <summary>
        /// 返回类型
        /// </summary>
        public Type ReturnType {
            get {
                return _action.ReturnType;
            }
            set {
                _action.ReturnType = value;
            }
        }

        public NAction Action {
            get {
                return this._action;
            }
        }
    }
}
