
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeClient.model
{
    [Serializable]
    public class Message
    {
        public string Title { get; set; }
        public Accounts Account { get; set; }

        public Result Result { get; set; }

        public MainEvent[] AllEvents { get; set; }

        public override string ToString()
        {
            return Title;
        }

    }
}
