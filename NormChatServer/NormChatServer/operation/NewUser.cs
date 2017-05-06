using NormChatServer.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormChatServer.operation
{
    public class NewUser : IExecute
    {
        public void Execute(Message message, ServerObject server, User user)
        {
            message.Id = user.Id;
            user.Name = message.Name;
            message.Msg = "Появляется " + user.Name;
            server.SendMessage(message, user);
        }
    }
}
