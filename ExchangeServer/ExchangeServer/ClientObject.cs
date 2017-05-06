using Base;
using ExchangeServer.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeServer
{
    public class ClientObject
    {
        protected internal string Id { get; private set; }
        private TcpClient client;
        private ServerObject server;
        protected internal NetworkStream Stream { get; private set; }

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
                // в бесконечном цикле получаем сообщения от клиента
                while (true)
                {
                    try
                    {
                        message = GetMessage();
                        Console.WriteLine(Id);
                        if (message != null)
                        {
                            RunMessage(message);
                            server.SendMessage(message, this.Id);
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
                server.RemoveConnection(this.Id);
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
            operation.Execute(this.Id);
        }
    }
}
