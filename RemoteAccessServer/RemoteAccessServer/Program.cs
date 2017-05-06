using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RemoteAccessServer
{
    class Program
    {
        static private DbLink link;
        static ServerObject server; // сервер
        static Thread listenThread; // потока для прослушивания
        static void Main(string[] args)
        {
            link = new DbLink();
            try
            {
                server = new ServerObject(link);
                listenThread = new Thread(new ThreadStart(server.Listen));
                listenThread.IsBackground = true;
                listenThread.Start();
            }
            catch (Exception ex)
            {
                server.Disconnect();
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();





        }
    }
}
