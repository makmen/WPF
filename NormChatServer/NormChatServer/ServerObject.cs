using NormChatServer.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NormChatServer
{
    public class ServerObject
    {
        static TcpListener tcpListener; // сервер для прослушивания
        private List<ClientObject> clients; // все подключения
        private bool start = true;

        public bool Start
        {
            get { return start; }
        }

        public ServerObject()
        {
            clients = new List<ClientObject>();
        }

        protected internal void AddConnection(ClientObject clientObject)
        {
            clients.Add(clientObject);
        }

        // прослушивание входящих подключений
        protected internal void Listen()
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, 8888);
                tcpListener.Start();
                Console.WriteLine("Сервер запущен. Ожидание подключений...");
                while (start)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();
                    ClientObject clientObject = new ClientObject(tcpClient, this);
                    Thread clientThread = new Thread(new ThreadStart(clientObject.Process));
                    clientThread.IsBackground = true;
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Disconnect();
            }
        }

        // трансляция сообщения одному клиенту
        protected internal void SendMessage(Message message, User user)
        {
            byte[] data = Serialization(message);
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].User.Id == user.Id)
                {
                    clients[i].Stream.Write(data, 0, data.Length);
                    break;
                }
            }
        }

        protected internal void SendMessageAllClients(Message message)
        {
            byte[] data = Serialization(message);
            for (int i = 0; i < clients.Count; i++)
            {
                clients[i].Stream.Write(data, 0, data.Length);
            }
        }

        public User[] TakeAllConnectedClients()
        {
            User[] usersArray = new User[clients.Count];
            for (int i = 0; i < clients.Count; i++)
            {
                usersArray[i] = clients[i].User;
            }

            return usersArray;
        }

        protected internal void RemoveConnection(User user)
        {
            ClientObject client = clients.FirstOrDefault(c => c.User.Id == user.Id);
            if (client != null)
            {
                client.Close();
                clients.Remove(client);
            }
        }

        protected internal void Disconnect()
        {
            start = false;
            tcpListener.Stop(); //остановка сервера
            for (int i = 0; i < clients.Count; i++)
            {
                clients[i].Close(); //отключение клиента
            }
        }

        public byte[] Serialization(Message obj)
        {
            MemoryStream stream = new MemoryStream();
            XmlSerializer formatter = new XmlSerializer(typeof(Message));
            formatter.Serialize(stream, obj);
            byte[] msg = stream.ToArray();
            stream.Close();

            return msg;
        }

        public Message DeSerialization(byte[] serializedAsBytes)
        {
            MemoryStream stream = new MemoryStream();
            XmlSerializer formatter = new XmlSerializer(typeof(Message));
            stream.Write(serializedAsBytes, 0, serializedAsBytes.Length);
            stream.Seek(0, SeekOrigin.Begin);
            Message newMsg = (Message)formatter.Deserialize(stream);
            stream.Close();

            return newMsg;
        }
    }
}
