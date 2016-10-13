using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public interface IHandleMessage<T>
    {
        void Handle(T msg);
    }
}
