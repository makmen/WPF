using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeClient.model
{
    [Serializable]
    public class Accounts
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string LoginSkype { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public System.DateTime DateRegister { get; set; }
    }
}
