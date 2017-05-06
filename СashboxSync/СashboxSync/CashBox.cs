using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СashboxSync
{
    public class CashBox
    {
        public int Id { get; set; }

        public int Nomer { get; set; }

        public CashBox()
        {
            Random rd = new Random();
            Id = rd.Next(1000, 99999);
            Nomer = rd.Next(1, 10);
        }

        public override string ToString()
        {
            return "Кассовый аппрат №" + Nomer + ", id = " + Id;
        }
    }
}
