using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo12
{
    public class MessageHandler
    {
        System.Guid g;
        public MessageHandler()
        {
            g = Guid.NewGuid();
            Console.WriteLine("MessageHandler GUID:" + g.ToString());

        }
        public void Show()
        {
            Console.WriteLine("MessageHandler GUID:" + g.ToString());
        }
    }
}
