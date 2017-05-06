using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteAccessServer.operation
{
    [Serializable]
    public class Message
    {
        public string Operation { get; set; }
        public string Request { get; set; }
        public string[] Result { get; set; } // все пользователи для листа
    }
}
