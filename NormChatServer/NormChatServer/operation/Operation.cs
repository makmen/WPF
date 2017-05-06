using NormChatServer.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormChatServer.operation
{
    public class Operation
    {
        private IExecute execute;
        private ServerObject server;
        private Message message;

        public Operation(Message message, ServerObject server)
        {
            this.message = message;
            this.server = server;
            switch (message.Operation)
            {
                case "newuser":
                    SetOperation(new NewUser());
                    break;
                case "newmessage":
                    SetOperation(new NewMessage());
                    break;
                case "broadcast":
                    SetOperation(new BroadCast());
                    break;
                case "updatelist":
                    SetOperation(new UpdateList());
                    break;
                default:
                    SetOperation(new Exit());
                    break;
            }
        }
        public void Execute(User user)
        {
            if (execute != null)
            {
                execute.Execute(message, server, user);
            }
        }

        public void SetOperation(IExecute execute)
        {
            this.execute = execute;
        }

    }
}
