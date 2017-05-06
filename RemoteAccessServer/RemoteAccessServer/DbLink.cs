using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace RemoteAccessServer
{
    public class DbLink
    {
        private DbConnection connection;

        public DbLink()
        {
            connection = null;
            string providerName = "";
            string strConnection = "";
            GetConnectionSettings(ref providerName, ref strConnection);
            if (providerName.Length > 0 && strConnection.Length > 0)
            {
                try
                {
                    DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);
                    connection = factory.CreateConnection();
                    connection.ConnectionString = strConnection;
                }
                catch (Exception)
                {
                    connection = null;
                }
            }
        }

        public void GetConnectionSettings(ref string providerName, ref string strConnection)
        {
            ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;
            if (settings != null)
            {
                foreach (ConnectionStringSettings cs in settings)
                {
                    providerName = cs.ProviderName;
                    strConnection = cs.ConnectionString;
                }
            }
        }

        public DbDataReader Select(string query)
        {
            DbDataReader dataReader = null;
            try
            {
                connection.Open();
                DbCommand command = connection.CreateCommand();
                command.CommandText = query;
                dataReader = command.ExecuteReader();
            }
            catch (Exception)
            {

            }

            return dataReader;
        }

        public int Execute(string query)
        {
            int result = -1;
            try
            {
                connection.Open();
                DbCommand command = connection.CreateCommand();
                command.CommandText = query;
                result = command.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        public void Close()
        {
            connection.Close();
        }
    }
}
