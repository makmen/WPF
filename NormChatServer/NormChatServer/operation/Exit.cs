using NormChatServer.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormChatServer.operation
{
    public class Exit : IExecute
    {
        public void Execute(Message message, ServerObject server, User user)
        {
            server.RemoveConnection(user);
            message.Msg = user.Name + " уходит из чата";
            message.Operation = "updatelist";
            message.users = server.TakeAllConnectedClients();
            server.SendMessageAllClients(message);
        }
    }
}
