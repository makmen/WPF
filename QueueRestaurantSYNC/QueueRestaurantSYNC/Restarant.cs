using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueRestaurantSYNC
{
    class Restarant
    {
        public string Name { get; set; }

        private List<CashBox> cashboxes;

        public List<CashBox> Cashboxes
        {
            get { return cashboxes; }
        }

        private int numberCashBox = 3;

        public int NumberCashBox
        {
            get { return numberCashBox; }
        }

        public Restarant(string name)
        {
            Name = name;
            cashboxes = new List<CashBox>();
            for (int i = 0; i < numberCashBox; i++)
            {
                cashboxes.Add(new CashBox(i + 1));
            }
        }

        public int GetBestQueue()
        {
            int index = 0, count = cashboxes[index].Queue.Count;

            for (int i = 0; i < numberCashBox; i++)
            {
                if (count > cashboxes[i].Queue.Count)
                {
                    count = cashboxes[i].Queue.Count;
                    index = i;
                }
            }

            return index;
        }

    }
}
