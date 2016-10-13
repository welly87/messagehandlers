using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messages;

namespace ClassLibrary1
{
    public class MessageBHandler : IHandleMessage<MessageB>
    {
        public void Handle(MessageB msg)
        {
            Console.WriteLine(msg);
        }
    }
}
