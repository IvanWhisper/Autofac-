using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo12
{
    public class Worker
    {
        System.Guid g;
        public Worker()
        {
             g= Guid.NewGuid();
        }
        public void DoWork()
        {
            Console.WriteLine(g.ToString());
        }
    }
}
