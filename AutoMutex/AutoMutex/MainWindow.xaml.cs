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

namespace AutoMutex
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Mutex mutex = new Mutex(false, "SYNC_MUTEX");
            for (int i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(Stop, mutex);
            }
        }

        public void Stop(object obj)
        {
            Mutex mutex = obj as Mutex;
            Autobus autobus = new Autobus(); // подъзжаем к остановки
            Write(autobus.ToString() + " подъезжает к остановке, поток: " + Thread.CurrentThread.ManagedThreadId + "\r\n");
            mutex.WaitOne();
            Write(autobus.ToString() + " совершает остановку, поток: " + Thread.CurrentThread.ManagedThreadId + "\r\n");
            Thread.Sleep(1000);
            Write(autobus.ToString() + " уезжает с остановки, поток: " + Thread.CurrentThread.ManagedThreadId + "\r\n");
            mutex.ReleaseMutex();
        }

        public void Write(string str)
        {
            tbBusStop.Dispatcher.BeginInvoke(new Action(delegate()
            {
                tbBusStop.Text += str;
            }));
        }
    }
}
