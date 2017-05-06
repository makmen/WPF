using System;
using System.Collections.Generic;
using System.Dynamic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace RemoteAccessClient
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
        private Message message;
        public Message Message
        {
            get { return message; }
            set { message = value; }
        }
        private bool active = true;
        public MainWindow()
        {
            InitializeComponent();
            message = new Message();
            message.Operation = "request";
            client = new TcpClient();
            try
            {
                client.Connect(host, port);
                stream = client.GetStream();
                // запускаем поток для приема сообщений
                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                receiveThread.IsBackground = true;
                receiveThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Disconnect();
                this.Close();
            }
        }

        private void btnSql_Click(object sender, RoutedEventArgs e)
        {
            if (ErrorControls.NotEmptyTextBox(tbRequest))
            {
                message.Request = tbRequest.Text;
                SendMessage(message);
            }
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
                    if (temp.Result != null)
                    {
                        HandlerMsg(temp);
                    }
                    else
                    {
                        MessageBox.Show("неправильный запрос");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    active = false;
                }
            }
        }
        // select * from accounts
        public void HandlerMsg(Message message)
        {
            List<string[]> listRows = new List<string[]>();
            string[] strArray = null;
            for (int i = 0; i < message.Result.Length; i++)
            {
                strArray = message.Result[i].Split('\t');
                listRows.Add(strArray);
            }
            if (listRows.Count > 0)
            {
                ShowSqlGrid(listRows);
            }
        }

        public void AddGridColumn(GridView myGridView, string header, string name)
        {
            GridViewColumn gvc1 = new GridViewColumn();
            gvc1.DisplayMemberBinding = new Binding(name);
            gvc1.Header = header;
            gvc1.Width = 100;
            myGridView.Columns.Add(gvc1);
        }
        /*
select accounts.id, orders.id
from accounts, orders
where accounts.Id = orders.Account_Id
select *
from accounts, orders
where accounts.Id = orders.Account_Id
         */

        public string DefineUnicName(string[] names, string need)
        {
            string result = need;
            int i = 0, j = 0;
            while (i < names.Length)
            {
                if (names[i] != result)
                {
                    ++i;
                }
                else
                {
                    result = need;
                    result += "_" + ++j;
                    i = 0;
                }
            }

            return result;
        }

        public void ShowSqlGrid(List<string[]> listRows)
        {
            lvResult.Dispatcher.BeginInvoke(new Action(delegate()
            {
                lvResult.Items.Clear();
                GridView myGridView = new GridView();
                myGridView.AllowsColumnReorder = true;
                myGridView.ColumnHeaderToolTip = "Result";
                int index = 0;
                string[] names = new string[listRows[index].Length];
                for (int j = 0; j < listRows[index].Length; j++)
                {
                    AddGridColumn(myGridView, listRows[index][j], listRows[index][j]);
                    names[j] = DefineUnicName(names, listRows[index][j]);
                }
                listRows.Remove(listRows[index]);
                lvResult.HorizontalAlignment = HorizontalAlignment.Center;
                lvResult.BorderBrush = Brushes.White;
                lvResult.View = myGridView;
                for (int i = 0; i < listRows.Count; i++)
                {
                    dynamic row = new ExpandoObject();
                    for (int j = 0; j < listRows[i].Length; j++)
                    {
                        ((IDictionary<String, Object>)row).Add(names[j], (object)listRows[i][j]);
                    }
                    lvResult.Items.Add(row);
                }
            }));
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
            if (stream != null && stream != null)
            {
                message.Operation = "exit";
                message.Result = null;
                SendMessage(message);
                Disconnect();
            }
        }
    }
}
