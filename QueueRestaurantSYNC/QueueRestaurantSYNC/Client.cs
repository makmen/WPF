using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueRestaurantSYNC
{
    public class Client
    {
        public int IdClient { get; set; }

        public int NomerInQueue { get; set; }

        public Client()
        {
            Random rd = new Random();
            IdClient = rd.Next(1000, 99999);
        }

        public override string ToString()
        {
            return "№" + NomerInQueue + ", IdClient = " + IdClient;
        }
    }
}
