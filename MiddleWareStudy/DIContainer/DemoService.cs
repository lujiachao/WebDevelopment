using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddleWareStudy.DIContainer
{
    public class DemoService1 : IDemoService
    {
        public string Version => "v1";

        public void Run()
        {
            Console.WriteLine("第一个服务实现类。");
        }
    }

    public class DemoSwevice2 : IDemoService
    {
        public string Version => "v2";

        public void Run()
        {
            Console.WriteLine("第二个服务实现类");
        }
    }
}
