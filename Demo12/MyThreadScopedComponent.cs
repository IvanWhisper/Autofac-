using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo12
{
    public class MyThreadScopedComponent
    {
        System.Guid g;
        public MyThreadScopedComponent()
        {
            g = Guid.NewGuid();
            Console.WriteLine("MyThreadScopedComponent构造函数:"+g.ToString());
        }
        public void Show()
        {
            Console.WriteLine("MyThreadScopedComponentShow:" + g.ToString());
        }
    }
}
