using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo12
{
    public class ServiceForHandler
    {
        System.Guid g;
        public ServiceForHandler()
        {
            g = Guid.NewGuid();
            Console.WriteLine("ServiceForHandler GUID:" + g.ToString());

        }
        public void Show()
        {
            Console.WriteLine("ServiceForHandler GUID:" + g.ToString());
        }

    }
}
