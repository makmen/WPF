using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteAccessServer.operation
{
    public interface IExecute
    {
        void Execute(Message message, ServerObject server, string id);
    }
}
