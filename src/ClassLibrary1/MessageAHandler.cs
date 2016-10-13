using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messages;

namespace ClassLibrary1
{
    public class MessageAHandler : IHandleMessage<MessageA>
    {
        public void Handle(MessageA msg)
        {
            Console.WriteLine(msg + "in MessageAHandler");
        }
    }
}
