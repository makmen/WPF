using NormChatServer.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormChatServer.operation
{
    public interface IExecute
    {
        void Execute(Message message, ServerObject server, User user);
    }
}
