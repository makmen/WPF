using NormChatClient.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormChatClient.operation
{
    public class Operation
    {
        private IExecute execute;

        public Operation(Message message)
        {
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
                    //SetOperation(new Exit());
                    break;
            }
        }

        public void Execute(Message message, ClientWindow parent)
        {
            if (execute != null)
            {
                execute.Execute(message, parent);
            }
        }

        public void SetOperation(IExecute execute)
        {
            this.execute = execute;
        }

    }
}
