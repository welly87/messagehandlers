using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messages;

namespace ClassLibrary1
{
    public class MessageA2Handler : IHandleMessage<MessageA>
    {
        public void Handle(MessageA msg)
        {
            Console.WriteLine(msg + "in MessageA2Handler");
        }
    }
}
