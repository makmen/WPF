using System;
using System.Collections.Generic;
using System.Linq;
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

namespace СashboxSync
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Queue<Client> queueClient;
        private CashBox cashBox;
        public Thread mainThread;

        public MainWindow()
        {
            InitializeComponent();
            queueClient = new Queue<Client>();
            ManualResetEvent eventManual = new ManualResetEvent(true);
            cashBox = new CashBox();

            ThreadStart threadStart = new ThreadStart(ThreadStart);
            mainThread = new Thread(threadStart);
            mainThread.Start();

            // создадим поток который будет обрабатывать очередь
            for (int i = 0; i < 4; i++)
            {
                ThreadPool.QueueUserWorkItem(QueueClient, eventManual); // ставим потоки в очередь
            }
        }

        public void QueueClient(object obj)
        {
            EventWaitHandle eventManual = obj as EventWaitHandle;
            Thread.Sleep(1000);
            eventManual.WaitOne(); // ставим поток в очередь 
            Client client = new Client(queueClient.Count + 1);
            queueClient.Enqueue(client);
            WriteToElement("Клиент: " + client.ToString() + " поставлен в очередь в кассу: " + cashBox.ToString() + "\r\n");
            eventManual.Set();
        }

        public void ThreadStart()
        {
            Client client = null;
            while(true)
            {
                lock (queueClient)
                {
                    client = (queueClient.Count > 0) ? queueClient.Dequeue() : null;
                }
                if (client != null)
                {
                    WriteToElement("Клиент: " + client.ToString() + " обрабатывается кассе: " + cashBox.ToString() + "\r\n");
                    Thread.Sleep(3000); // обрабатываем клиента
                    WriteToElement("Клиент: " + client.ToString() + " уходит из кассы: " + cashBox.ToString() + "\r\n");
                }
                else
                {
                    WriteToElement("Ждем клиента...\r\n");
                    Thread.Sleep(3000); // ждем посетителя
                }
            }
        }

        public void WriteToElement(string str)
        {
            tbCashBox.Dispatcher.BeginInvoke(new Action(delegate()
            {
                tbCashBox.Text += str;
            }));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainThread.Abort();
        }
    }
}
