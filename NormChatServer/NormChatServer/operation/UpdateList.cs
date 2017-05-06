using NormChatServer.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormChatServer.operation
{
    public class UpdateList : IExecute
    {
        public void Execute(Message message, ServerObject server, User user)
        {
            message.users = server.TakeAllConnectedClients();
            message.Msg = "Пользователь " + message.Name + " вошел в чат";
            server.SendMessageAllClients(message);
        }
    }
}
