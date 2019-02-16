using Itcast.Mall.SQL;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itcast.Mall
{
    public static class ExtensionDispatch
    {
        public static void AddDispatch(this IServiceCollection serviceCollection)
        {
            #region DAL层添加到依赖注入框架
            serviceCollection.AddSingleton(typeof(CaptchaDAL));
            #endregion
        }
    }
}
