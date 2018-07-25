using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo15
{
    public class NewWorker : IService
    {
        System.Guid g;
        public NewWorker()
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
