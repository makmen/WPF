using ExchangeClient.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
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

namespace ExchangeClient
{
    /// <summary>
    /// Логика взаимодействия для RunWindow.xaml
    /// </summary>
    public partial class RunWindow : Window
    {
        private MainWindow parent;
        private List<MainEvent> listEvents;
        private List<MainEvent> listResultEvents;
        public List<MainEvent> ListResultEvents
        {
            get { return listResultEvents; }
        }
        private int threadDelay = 0;
        Queue<KeyValuePair<int, int>> valueList;
        private int MaxQueue = 20;
        private int eventMessage = 0;

        public RunWindow()
        {
            InitializeComponent();
            listEvents = new List<MainEvent>();
            listResultEvents = new List<MainEvent>();
            GetDelayThread();
            valueList = new Queue<KeyValuePair<int, int>>();
            for (int i = 0; i < MaxQueue; i++)
            {
                valueList.Enqueue(new KeyValuePair<int, int>(i, 0));
            }
            AreaSeries.DataContext = valueList;
            Thread runThread = new Thread(new ThreadStart(RunningThread));
            runThread.IsBackground = true;
            runThread.Start();
        }

        public void GetDelayThread()
        {
            string str = ((ComboBoxItem)cmbUpdate.SelectedItem).ToString();
            foreach (Match match in Regex.Matches(str, @"\d+", RegexOptions.IgnoreCase))
            {
                if (Int32.TryParse(match.Value, out threadDelay))
                {
                    break;
                }
            }
        }
        private void cmbUpdate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetDelayThread();
        }

        public void RunningThread()
        {
            int i = MaxQueue;
            Message message = new Message();
            message.Title = "keepeya";
            while(true) 
            {
                if (listEvents.Count > 0) // делаем запрос к БД
                {
                    message.Result = null;
                    message.Account = null;
                    message.AllEvents = new MainEvent[listEvents.Count];
                    for (int index = 0; index < listEvents.Count; index++)
			        {
                        message.AllEvents[index] = listEvents[index];
			        }
                    Message recieveMessage = parent.SendToServer(message);
                    if (recieveMessage != null && recieveMessage.Result != null)
                    {
                        for (int index = 0; index < recieveMessage.AllEvents.Length; index++)
                        {
                            if (recieveMessage.AllEvents[index].EventDone || recieveMessage.AllEvents[index].EventError != "") 
                                // если событие произошло 
                            {
                                MainEvent temp = listEvents.FirstOrDefault(
                                    c => c.Title == recieveMessage.AllEvents[index].Title
                                    && c.Sign == recieveMessage.AllEvents[index].Sign
                                    && c.ValueParam == recieveMessage.AllEvents[index].ValueParam
                                );
                                if (recieveMessage.AllEvents[index].EventError == "")
                                {
                                    Interlocked.Increment(ref eventMessage);
                                    // отправляем еmail
                                    SendEmail sendMail = new SendEmail(parent.MyAccount.Email, "Событие произошло", temp.ToString());
                                    // отправляем сообщение по Skype
                                    SendSkype sendSkype = new SendSkype(parent.MyAccount.LoginSkype, temp.ToString());
                                    listResultEvents.Add(temp);
                                }
                                else
                                {
                                    MessageBox.Show(recieveMessage.AllEvents[index].EventError + " " + recieveMessage.AllEvents[index].Title);
                                }
                                // удаляем событие
                                listEvents.Remove(temp);
                                UpdateListEvent();
                            }
                        }
                    }
                }
                valueList.Enqueue(new KeyValuePair<int, int>((++i), eventMessage));
                if (valueList.Count > MaxQueue)
                {
                    valueList.Dequeue();
                }
                Message(i);
                Thread.Sleep(threadDelay * 1000);
            }
        }

        public void UpdateListEvent()
        {
            lstEvents.Dispatcher.BeginInvoke(new Action(delegate()
            {
                lstEvents.ItemsSource = null;
                lstEvents.ItemsSource = listEvents;
            }));
        }

        public void Message(int i)
        {
            btnRes.Dispatcher.BeginInvoke(new Action(delegate()
            {
                if (eventMessage > 0)
                {
                    btnRes.Content = eventMessage;
                }
            }));
            AreaSeries.Dispatcher.BeginInvoke(new Action(delegate()
            {
                AreaSeries.DataContext = null;
                AreaSeries.DataContext = valueList;
            }));
        }

        public void AddEvent(MainEvent mainEvent)
        {
            lstEvents.ItemsSource = null;
            listEvents.Add(mainEvent);
            lstEvents.ItemsSource = listEvents;
            lstEvents.SelectedItem = listEvents[(listEvents.Count - 1)];
        }

        public void EditEvent()
        {
            lstEvents.ItemsSource = null;
            lstEvents.ItemsSource = listEvents;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            parent = (MainWindow)this.Owner;
            lbWelcome.Content = "Добро пожаловать, " + parent.MyAccount.Login;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            parent.Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            EventWindow eventWindow = new EventWindow();
            eventWindow.Owner = this;
            eventWindow.Show();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            MainEvent mainEvent = (MainEvent)lstEvents.SelectedItem;
            if (mainEvent != null)
            {
                EventWindow eventWindow = new EventWindow("edit", mainEvent);
                eventWindow.Owner = this;
                eventWindow.Show();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstEvents.SelectedIndex >= 0)
            {
                MainEvent mainEvent = (MainEvent)lstEvents.SelectedItem;
                MessageBoxResult res = MessageBox.Show("Вы уверены что хотите удалить это событие: " + mainEvent.ToString(), "Удалить событие", MessageBoxButton.OKCancel);
                if (res == MessageBoxResult.OK)
                {
                    listEvents.Remove(mainEvent);
                    EditEvent();
                }
            }
        }

        private void btnRes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int count = Int32.Parse(btnRes.Content.ToString());
                Interlocked.Add(ref eventMessage, -count);
                btnRes.Content = (eventMessage > 0) ? eventMessage.ToString() : "";
            }
            catch
            {

            }
            ShowResultEventWindow resultWindow = new ShowResultEventWindow();
            resultWindow.Owner = this;
            resultWindow.Show();
        }

    }
}
