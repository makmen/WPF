using NormChatClient.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormChatClient.operation
{
    public interface IExecute
    {
        void Execute(Message message, ClientWindow parent);
    }
}
