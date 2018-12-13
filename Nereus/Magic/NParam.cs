using System;

namespace Nereus
{
    public class NParam {
        /// <summary>
        /// 参数名
        /// </summary>
        public string Code {
            get; set;
        }

        /// <summary>
        /// 中文解释
        /// </summary>
        public string Name {
            get; set;
        }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue {
            get; set;
        }

        /// <summary>
        /// 参数类型
        /// </summary>
        public Type ParamType {
            get; set;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class NParamAttribute : Attribute {

        public NParamAttribute(string name) {
            this.Name = name;
        }

        private NParam _param = new NParam();

        public string Name {
            get {
                return _param.Name;
            }
            set {
                _param.Name = value;
            }
        }
    }
}
