using InterfaceLib06;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib06
{
    public class MyRepository : IRes
    {
        public MyRepository()
        {
            Console.WriteLine("构造");
        }
        public void show()
        {
            Console.WriteLine("SHow");
        }
    }
}
