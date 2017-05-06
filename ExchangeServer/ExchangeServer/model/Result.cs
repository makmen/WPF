using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeServer.model
{
    [Serializable]
    public class Result
    {
        public int Code { get; set; } // если 0 ошибок не было
        public string TitleError { get; set; } // текст ошибки
    }
}
