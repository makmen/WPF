using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeClient.model
{
    [Serializable]
    public class MainEvent
    {
        public string Title { get; set; }

        public string Sign { get; set; }

        public Param MainParam { get; set; }

        public double ValueParam { get; set; }

        public bool EventDone { get; set; }

        public string EventError { get; set; }

        public override string ToString()
        {
            return Title + " " + MainParam.Name + " " + ValueParam.ToString();
        }
    }
}
