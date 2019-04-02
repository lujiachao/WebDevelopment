using Microsoft.Extensions.DependencyInjection;
using PrivilegeManagement.AttributeExtension;
using PrivilegeManagement.Common.Enum;
using PrivilegeManagement.Dispatchs;
using System;
using System.Linq;
using System.Reflection;

namespace PrivilegeManagement
{
    public static class ExtensionDispatch
    {
        public static void LoadSingleton(this IServiceCollection serviceCollection)
        {
            var singletonAttribute = Assembly.GetAssembly(typeof(DIUserAttribute));
            Type[] types = singletonAttribute.GetExportedTypes();
            Func<Attribute[], bool> IsSingletonAttribute = o =>
            {
                foreach (Attribute a in o)
                {
                    if (a is DIUserAttribute)
                        return true;
                }
                return false;
            };

            Type[] cosType = types.Where(o =>
            {
                return IsSingletonAttribute(Attribute.GetCustomAttributes(o, true));
            }).ToArray();
            if (cosType != null && cosType.Count() > 0)
            {
                foreach (var item in cosType)
                {
                    var enumDIUserImport = (int)item.GetCustomAttribute(typeof(DIUserAttribute)).GetType().GetProperty("EnumDIUserImport").GetValue(item.GetCustomAttribute(typeof(DIUserAttribute)));
                    switch (enumDIUserImport)
                    {
                        case (int)EnumTypeDIImport.Singleton:
                            serviceCollection.AddSingleton(item);
                            break;
                        case (int)EnumTypeDIImport.Scoped:
                            serviceCollection.AddScoped(item);
                            break;
                        default:
                            serviceCollection.AddTransient(item);
                            break;
                    }
                }
            }
        }
    }
}
