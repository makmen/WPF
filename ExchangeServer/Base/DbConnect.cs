using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    public class DbConnect
    {
        private killEntities dbLink;

        public killEntities DbLink
        {
            get { return dbLink; }
        }

        public DbConnect()
        {
            dbLink = new killEntities();
        }

        public bool AddAccount(Accounts account)
        {
            try
            {
                dbLink.Accounts.Add(account);
                dbLink.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public Accounts GetAccount(string login, string password)
        {
            Accounts singleTask =
                (from account in dbLink.Accounts
                 where account.Login == login && account.Password == password
                 select account).FirstOrDefault();

            return singleTask;
        }

        public Parameters GetParameter(string paramTitle)
        {
            dbLink = new killEntities();
            Parameters singleParam =
                (from param in dbLink.Parameters
                 where param.Title == paramTitle
                 select param).FirstOrDefault();

            return singleParam;
        }

    }
}
