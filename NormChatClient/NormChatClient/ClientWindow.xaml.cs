using NormChatClient.model;
using NormChatClient.operation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace NormChatClient
{
    /// <summary>
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        private MainWindow parent;
        private const string host = "127.0.0.1";
        private const int port = 8888;
        private TcpClient client;
        private NetworkStream stream;
        private Message message;
        public Message Message
        {
            get { return message; }
            set { message = value; }
        }
        private bool active = true;
        private List<User> listClients;
        public List<User> ListClients
        {
            get { return listClients; }
        }

        public ClientWindow(string userName)
        {
            InitializeComponent();
            message = new Message();
            message.Name = userName;
            client = new TcpClient();
            listClients = new List<User>();
            try
            {
                client.Connect(host, port);
                stream = client.GetStream();

                message.Operation = "newuser";
                SendMessage(message);

                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage)); // запускаем поток для приема сообщений
                receiveThread.IsBackground = true;
                receiveThread.Start();
            }
            catch (Exception ex)
            {
                WriteText(ex.Message);
                Disconnect();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            User userTo = (User)lstClients.SelectedItem;
            if (userTo != null)
            {
                if (ErrorControls.NotEmptyTextBox(tbMsg))
                {
                    message.IdTo = userTo.Id;
                    message.Msg = tbMsg.Text;
                    message.Operation = "newmessage";
                    message.users = null;
                    tbResult.Text += message.Name + ": " + message.Msg + "\r\n";
                    SendMessage(message);
                    tbMsg.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Выберите пользователя");
            }
        }

        public void SendMessage(object obj)
        {
            message = obj as Message;
            byte[] data = Serialization(message);
            stream.Write(data, 0, data.Length);
        }

        public void ReceiveMessage()
        {
            Message temp = null;
            while (active)
            {
                try
                {
                    byte[] data = new byte[1024]; // буфер для получаемых данных
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                    }
                    while (stream.DataAvailable);
                    temp = DeSerialization(data);
                    if (temp != null)
                    {
                        analizeMsq(temp);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    active = false;
                }
            }
            CloseWindow();
        }

        public void analizeMsq(Message msg)
        {
            Operation operation = new Operation(msg);
            operation.Execute(msg, this);
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            parent = (MainWindow)this.Owner;
        }

        private void Disconnect()
        {
            if (stream != null)
            {
                stream.Close();
            }
            if (client != null)
            {
                client.Close();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            message.Operation = "exit";
            SendMessage(message);
            Disconnect();
            parent.Close();
        }

        public void CloseWindow()
        {
            this.Dispatcher.BeginInvoke(new Action(delegate()
            {
                this.Close();
            }));
        }

        public void UpdateList()
        {
            lstClients.Dispatcher.BeginInvoke(new Action(delegate()
            {
                lstClients.ItemsSource = null;
                lstClients.ItemsSource = listClients;
            }));
        }

        public void WriteText(string str)
        {
            tbResult.Dispatcher.BeginInvoke(new Action(delegate()
            {
                tbResult.Text += str + "\r\n";
            }));
        }
    }
}
