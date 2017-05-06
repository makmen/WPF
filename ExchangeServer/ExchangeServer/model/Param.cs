using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeServer.model
{
    [Serializable]
    public class Param
    {
        public string Name { get; set; }
        public string NameInBase { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
