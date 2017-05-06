using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteAccessServer.operation
{
    public class Request : IExecute
    {
        public void Execute(Message message, ServerObject server, string id)
        {
            int restultOperation = -1;
            if (defineRequest(message.Request))
            {
                DbDataReader dataReader = server.Link.Select(message.Request);
                message.Result = handler(dataReader);
                server.Link.Close();
            }
            else
            {
                restultOperation = server.Link.Execute(message.Request);
            }
            server.SendMessage(message, id);
        }

        public bool defineRequest(string str)
        {
            bool selected = true;
            // определяем что Select

            return selected;
        }

        public string[] handler(DbDataReader dataReader)
        {
            string[] nameColumn = null, result = null;
            List<string> listRows = new List<string>();
            if (dataReader != null)
            {
                nameColumn = new string[dataReader.FieldCount];
                for (int i = 0; i < nameColumn.Length; i++)
                {
                    nameColumn[i] = dataReader.GetName(i);
                }
                string temp = string.Join("\t", nameColumn);
                listRows.Add(temp);
                while (dataReader.Read())
                {
                    string[] rows = new string[dataReader.FieldCount];
                    for (int i = 0; i < nameColumn.Length; i++)
                    {
                        rows[i] = dataReader[nameColumn[i]].ToString();
                    }
                    temp = string.Join("\t", rows);
                    listRows.Add(temp);
                }
            }
            if (listRows.Count > 0)
            {
                result = new string[listRows.Count];
                for (int i = 0; i < listRows.Count; i++)
                {
                    result[i] = listRows[i];
                }
            }

            return result;
        }
    }
}
