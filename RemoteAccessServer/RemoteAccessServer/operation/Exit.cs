using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteAccessServer.operation
{
    public class Exit : IExecute
    {
        public void Execute(Message message, ServerObject server, string id)
        {
            server.RemoveConnection(id);
        }
    }
}
