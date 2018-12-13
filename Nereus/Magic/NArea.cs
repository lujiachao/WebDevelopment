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

    /// <summary>
    /// mvc 区域属性描述
    /// </summary>
    public class NArea {

        public string Code {
            get; set;
        }

        public string Name {
            get; set;
        }

        public string Description {
            get; set;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class NAreaAttribute : Attribute {

        private NArea _area = new NArea();

        public string Code {
            get {
                return _area.Code;
            }
            set {
                _area.Code = value;
            }
        }

        public string Name {
            get {
                return _area.Name;
            }
            set {
                _area.Name = value;
            }
        }

        public string Description {
            get {
                return _area.Description;
            }
            set {
                _area.Description = value;
            }
        }

        public NArea Area {
            get {
                return this._area;
            }
        }
    }
}
