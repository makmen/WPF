using NormChatServer.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormChatServer.operation
{
    
    public class BroadCast : IExecute
    {
        public void Execute(Message message, ServerObject server, User user)
        {
            server.SendMessageAllClients(message);
        }
    }
}
