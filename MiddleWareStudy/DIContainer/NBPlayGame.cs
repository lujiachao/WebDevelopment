using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddleWareStudy.DIContainer
{
    public class NBPlayGame : IPlayGame
    {
        public void Play()
        {
            Console.WriteLine("全民打麻药。");
        }
    }
}
