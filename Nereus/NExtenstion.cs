//**************************************
// Author              :    DannyShen
// Email               :    dannyshenl@163.com
// Create Time         :    2016/6/12 15:43:18
// Update Time         :    2016/6/12 15:43:18
// *************************************
// CLR Version         :    4.0.30319.42000
// Class Version       :    v1.0.0.0
// Class Description   :    
// *************************************
// Copyright ©SL 2016 . All rights reserved.
// *************************************
using System;
using System.Reflection;
using System.Text;

namespace Nereus {
    public static class NExtenstion {
        public static T GetNAttribute<T>(this Type type) where T : Attribute {
            object[] customAttributes = type.GetCustomAttributes(typeof(T), false);
            if(customAttributes.Length > 0) {
                return (customAttributes[0] as T);
            }
            return default(T);
        }

        public static T GetNAttribute<T>(this MethodInfo mi) where T : Attribute {
            object[] customAttributes = mi.GetCustomAttributes(typeof(T), false);
            if(customAttributes.Length > 0) {
                return (customAttributes[0] as T);
            }
            return default(T);
        }

        public static T GetNAttribute<T>(this PropertyInfo pi) where T : Attribute {
            object[] customAttributes = pi.GetCustomAttributes(typeof(T), false);
            if(customAttributes.Length > 0) {
                return (customAttributes[0] as T);
            }
            return default(T);
        }

        public static StringBuilder AppendLineHtml(this StringBuilder sb) {
            return sb.Append("<br />");
        }

        public static StringBuilder AppendLineHtml(this StringBuilder sb, string html) {
            return sb.Append(string.Format("{0}<br />", html));
        }
    }
}
