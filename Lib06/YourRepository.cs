using InterfaceLib06;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib06
{
    public class YourRepository:IRes
    {
        public YourRepository()
        {
            Console.WriteLine("Your构造");
        }
        public void show()
        {
            Console.WriteLine("your SHow");
        }

    }
}
