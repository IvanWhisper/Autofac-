using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;

namespace Demo15
{
    public class Worker :IService
    {
        System.Guid g;
        public Worker()
        {
            g = Guid.NewGuid();
            Console.WriteLine("Worker GUID:" + g.ToString());

        }
        public void Show()
        {
            Console.WriteLine("Worker GUID:" + g.ToString());
        }

    }
}
