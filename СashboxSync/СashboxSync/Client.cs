using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СashboxSync
{
    public class Client
    {
        public int IdClient { get; set; }

        public int NomerInQueue { get; set; }

        public Client(int NomerInQueue )
        {
            Random rd = new Random();
            IdClient = rd.Next(1000, 99999);
            this.NomerInQueue = NomerInQueue;
        }

        public override string ToString()
        {
            return "Клиента №" + NomerInQueue + ", IdClient = " + IdClient;
        }
    }
}
