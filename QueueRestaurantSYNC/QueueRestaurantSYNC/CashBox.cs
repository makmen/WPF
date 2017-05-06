using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueueRestaurantSYNC
{
    public class CashBox
    {
        private Queue<Client> queue;

        public Queue<Client> Queue
        {
            get { return queue; }
        }
        public int Id { get; set; }

        public int Nomer { get; set; }

        public CashBox(int nomer)
        {
            Random rd = new Random();
            Thread.Sleep(10);
            Id = rd.Next(1000, 99999);
            Nomer = nomer;
            queue = new Queue<Client>();
        }

        public override string ToString()
        {
            return "№" + Nomer + ", idCashBox = " + Id;
        }
    }
}
