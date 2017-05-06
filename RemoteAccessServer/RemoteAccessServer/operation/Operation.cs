using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteAccessServer.operation
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
                case "request":
                    SetOperation(new Request());
                    break;
                default:
                    SetOperation(new Exit());
                    break;
            }
        }
        public void Execute(string id)
        {
            if (execute != null)
            {
                execute.Execute(message, server, id);
            }
        }

        public void SetOperation(IExecute execute)
        {
            this.execute = execute;
        }

    }
}
