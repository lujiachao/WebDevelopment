using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyDapper.SqlPower
{
    public static class EntityHelper
    {
        public static bool IsInterface(this Type type) =>
#if NETSTANDARD1_3 || NETCOREAPP1_0
            type.GetTypeInfo().IsInterface;
#else
            type.IsInterface;
#endif

        /// <summary>
        /// 获取实体映射表名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string CallName<T>()
        {
            var type = typeof(T);
            var name = GetTableName(type);
            return name;

        }

        public static string GetTableName(Type type)
        {
            string name;

            var info = type;

            var tableAttrName = (info.GetCustomAttributes(false).FirstOrDefault(attr => attr.GetType().Name == "TableAttribute") as dynamic)?.Name;

            if (tableAttrName != null)
            {
                name = tableAttrName;
            }
            else
            {
                name = type.Name + "s";
                if (type.IsInterface() && name.StartsWith("I"))
                    name = name.Substring(1);
            }
            return name;
        }
    }
}
