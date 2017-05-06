using NormChatClient.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormChatClient.model
{
    [Serializable]
    public class Message
    {
        public string Id { get; set; }
        public string IdTo { get; set; }
        public string Name { get; set; }
        public string Msg { get; set; }
        public string Operation { get; set; }
        public User[] users { get; set; } // все пользователи для листа

        public override string ToString()
        {
            return Id + " " + Name;
        }
    }
}
