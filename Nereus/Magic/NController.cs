//**************************************
// Author              :    DannyShen
// Email               :    dannyshenl@163.com
// Create Time         :    2016/6/7 16:40:14
// Update Time         :    2016/6/7 16:40:14
// *************************************
// CLR Version         :    4.0.30319.42000
// Class Version       :    v1.0.0.0
// Class Description   :    
// *************************************
// Copyright ©SL 2016 . All rights reserved.
// *************************************
using System;
using System.Collections.Generic;

namespace Nereus {
    public class NController {
        public NController() {
            this.Actions = new List<NAction>();
        }

        public string Name {
            get; set;
        }

        public string Description {
            get; set;
        }

        public string Code {
            get; set;
        }

        public NArea Area {
            get; set;
        }

        public List<NAction> Actions {
            get; set;
        }
         
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class NControllerAttribute : Attribute {

        private NController _controller = new NController();

        public string Name {
            get {
                return this._controller.Name;
            }
            set {
                this._controller.Name = value;
            }
        }

        public string Description {
            get {
                return this._controller.Description;
            }
            set {
                this._controller.Description = value;
            }
        }

        public string Code {
            get {
                return this._controller.Code;
            }
            set {
                this._controller.Code = value;
            }
        }

        public NController Controller {
            get {
                return _controller;
            }
        }
    }
}
