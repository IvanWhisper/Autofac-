using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo12
{
    public class SingleWorker
    {
        System.Guid g;
        public SingleWorker()
        {
            g = Guid.NewGuid();
        }
        public void DoWork()
        {
            Console.WriteLine("SingleID:"+g.ToString());
        }
    }
}
