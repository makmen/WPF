using NormChatClient.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormChatClient.operation
{
    public class UpdateList : IExecute
    {
        public void Execute(Message message, ClientWindow parent)
        {
            if (message.users != null && message.users.Length > 0)
            {
                parent.ListClients.Clear();
                for (int i = 0; i < message.users.Length; i++)
                {
                    if (message.users[i].Id != parent.Message.Id)
                    {
                        parent.ListClients.Add(message.users[i]);
                    }
                }
                parent.WriteText(message.Msg);
                parent.UpdateList();
            }

        }
    }
}
