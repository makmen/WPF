using NormChatClient.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormChatClient.operation
{
    public class NewUser : IExecute
    {
        public void Execute(Message message, ClientWindow parent)
        {
            parent.Message.Id = message.Id;
            message.Operation = "updatelist";    // Запросить список всех подключений
            parent.SendMessage(message);
        }
    }
}
