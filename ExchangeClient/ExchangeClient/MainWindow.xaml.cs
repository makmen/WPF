using ExchangeClient.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace ExchangeClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string host = "127.0.0.1";
        private const int port = 8888;
        private TcpClient client;
        private NetworkStream stream;
        private Accounts myAccount;

        public Accounts MyAccount
        {
            get { return myAccount; }
            set { myAccount = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            int res = CheckUser();
            if (res == 0)
            {
                Accounts account = new Accounts();
                account.Login = tbLogin.Text;
                account.Password = tbPass.Password;
                Message message = new Message();
                message.Title = "checkin";
                message.Account = account;
                message.Result = null;

                Message recieveMessage = SendToServer(message);
                if (recieveMessage != null && recieveMessage.Result != null)
                {
                    if (recieveMessage.Result.Code == 0)
                    {
                        this.Hide();
                        message.Account.LoginSkype = recieveMessage.Account.LoginSkype;
                        message.Account.Email = recieveMessage.Account.Email;
                        message.Account.Id = recieveMessage.Account.Id;
                        message.Account.DateRegister = recieveMessage.Account.DateRegister;
                        myAccount = message.Account;
                        RunWindow run = new RunWindow();
                        run.Owner = this;
                        run.Show();
                    }
                    else
                    {
                        tbPass.Password = "";
                        MessageBox.Show(recieveMessage.Result.TitleError);
                    }
                }
                else
                {
                    MessageBox.Show("Нет соединения с сервером");
                }
            }
        }

        private int CheckUser()
        {
            bool check;
            int i = 0;
            check = ErrorControls.NotEmptyTextBox(tbLogin) &&
                ErrorControls.RegexTextBox(tbLogin, "^[a-z]+$");
            ErrorControls.CountErrors(ref i, check);
            lbError.Visibility = (check) ? Visibility.Hidden : Visibility.Visible;

            check = ErrorControls.NotEmptyPasswordBox(tbPass);
            ErrorControls.CountErrors(ref i, check);

            return i;
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RegisterWindow addClient = new RegisterWindow();
            addClient.Owner = this;
            addClient.Show();
        }

        public Message SendToServer(Message message)
        {
            Message recieveMessage = null;
            client = new TcpClient();
            try
            {
                client.Connect(host, port);
                stream = client.GetStream();
                SendMessage(message);
                recieveMessage = ReceiveMessage();
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message);
            }

            return recieveMessage;
        }

        public void SendMessage(Message message)
        {
            byte[] data = Serialization(message);
            stream.Write(data, 0, data.Length);
        }

        public Message ReceiveMessage()
        {
            Message temp = null;
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
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return temp;
        }

        public void Disconnect()
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

        private void Window_Closed(object sender, EventArgs e)
        {
            if (client != null)
            {
                Message message = new Message();
                message.Title = "exit";
                SendMessage(message);

                stream.Close();
                client.Close();
            }
        }
    }
}
