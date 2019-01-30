using Microsoft.Extensions.DependencyInjection;
using PrivilegeManagement.Dispatchs;

namespace PrivilegeManagement
{
    public static class ExtensionDispatch
    {
        public static void AddDispatch(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(typeof(PrivilegeUserDispatch));
            serviceCollection.AddSingleton(typeof(UserDispatch));
        }
    }
}
