using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMutex
{
    class Autobus
    {
        public int Nomer { get; set; }

        public int gosNomer { get; set; }

        public Autobus()
        {
            Random rd = new Random();
            gosNomer = rd.Next(1000, 9999);
            rd = new Random();
            Nomer = rd.Next(1, 99);
        }

        public override string ToString()
        {
            return "Autobus №" + Nomer + " Gos Number:" + gosNomer;
        }

    }
}
