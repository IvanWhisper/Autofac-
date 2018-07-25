using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo10
{
    public class MessageProcessor
    {
        private IEnumerable<IMessageHandler> _handlers;
        public MessageProcessor(IEnumerable<IMessageHandler> handlers)
        {
            this._handlers = handlers;
        }
        public void ProcessMessage(Message m)
        {
            foreach (var handler in this._handlers)
            {
                handler.HandleMessage(m);
            }
        }
    }
}
