using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueueRestaurantSYNC
{
    class Program
    {
        static private Restarant restarant;
        static Random rd;

        static void Main(string[] args)
        {
            // создаем ресторан
            restarant = new Restarant("Желтые тюльпаны");
            Console.WriteLine("Добро пожаловать в ресторан быстрого питания " + restarant.Name);
            List<Thread> listMainThread = new List<Thread>();
            rd = new Random();
            ParameterizedThreadStart threadStart = new ParameterizedThreadStart(ThreadStart);
            // на каждую очередь вешаем поток
            for (int i = 0; i < restarant.NumberCashBox; i++)
            {
                listMainThread.Add(new Thread(threadStart));
                listMainThread[i].Start((object)restarant.Cashboxes[i]);// поток обрабатывает клиентов
            }
            int index = 3;
            while(index > 0)
            {
                for (int i = 0; i < 20; i++)
                {
                    ThreadPool.QueueUserWorkItem(QueueClient); // ставим потоки в очередь
                    Thread.Sleep(rd.Next(100, 1000));// каждую секу приходит новый клиент
                }
                Thread.Sleep(5000);
                --index;
            }

        }

        static public void QueueClient(object obj)
        {
            Client client = new Client();
            int index = restarant.GetBestQueue();// ищем самую быструю очередь

            lock (restarant.Cashboxes[index].Queue) // лочим очередь
            {
                restarant.Cashboxes[index].Queue.Enqueue(client); // добавляем клиента в очередь
                client.NomerInQueue = restarant.Cashboxes[index].Queue.Count;
            }
            Console.WriteLine("Клиент: " + client.ToString() +
                " есть в очереди (" + client.NomerInQueue + ") в кассу " + 
                restarant.Cashboxes[index].ToString());
        }

        static public void ThreadStart(object obj)
        {
            CashBox casBox = obj as CashBox;
            Client client = null;
            int number = 10;     // максимум 10 попыток
            while (number > 0)
            {
                lock (casBox.Queue)
                {
                    client = (casBox.Queue.Count > 0) ? casBox.Queue.Dequeue() : null;
                }
                if (client != null)
                {
                    Console.WriteLine("Клиент: " + client.ToString() +
                        " обрабатывается в кассе " + casBox.ToString());
                    Thread.Sleep(rd.Next(3000, 4000)); // обрабатываем клиента
                    Console.WriteLine("Клиент: " + client.ToString() + 
                        " уходит из кассы " + casBox.ToString());
                    Thread.Sleep(rd.Next(5, 100));
                }
                else
                {
                    Console.WriteLine("Ждем клиента... Касса " + casBox.ToString());
                    Thread.Sleep(1000); // ждем посетителя
                    --number;
                }
            }
        }

    }
}
