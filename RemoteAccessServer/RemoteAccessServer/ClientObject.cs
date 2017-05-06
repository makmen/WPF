using RemoteAccessServer.operation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RemoteAccessServer
{
    public class ClientObject
    {
        protected internal NetworkStream Stream { get; private set; }
        protected internal string Id { get; private set; }
        private TcpClient client;
        private ServerObject server;

        public ClientObject(TcpClient tcpClient, ServerObject serverObject)
        {
            Id = Guid.NewGuid().ToString();
            client = tcpClient;
            server = serverObject;
            serverObject.AddConnection(this);
        }

        public void Process()
        {
            try
            {
                Stream = client.GetStream();
                Message message = null;
                while (server.Start)
                {
                    try
                    {
                        message = GetMessage();
                        Console.WriteLine(Id);
                        if (message != null)
                        {
                            RunMessage(message);
                        }
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                server.RemoveConnection(Id);
                Close();
            }
        }

        // чтение входящего сообщения и преобразование в строку
        private Message GetMessage()
        {
            byte[] data = new byte[1024]; // буфер для получаемых данных
            int bytes = 0;
            do
            {
                bytes = Stream.Read(data, 0, data.Length);
            }
            while (Stream.DataAvailable);
            Message temp = server.DeSerialization(data);

            return temp;
        }

        // закрытие подключения
        protected internal void Close()
        {
            if (Stream != null)
                Stream.Close();
            if (client != null)
                client.Close();
        }

        public void RunMessage(Message message) 
        {
            Operation operation = new Operation(message, server);
            operation.Execute(Id);
        }
    }
}
