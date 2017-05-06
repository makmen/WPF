using ExchangeClient.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для EventWindow.xaml
    /// </summary>
    public partial class EventWindow : Window
    {
        private RunWindow parent;
        private List<Param> listParam;
        private string mode;
        private RadioButton pressed;
        private MainEvent mainEvent;

        public EventWindow(string mode = "add", MainEvent mainEvent = null)
        {
            InitializeComponent();
            listParam = new List<Param>()
            {
                new Param() {Name = "Цена", NameInBase = "Price"},
                new Param() {Name = "Количество сделок", NameInBase = "AmountDeals"},
                new Param() {Name = "Объем сделок", NameInBase = "VolumeDeals"},
                new Param() {Name = "Волатильность", NameInBase = "Volatility"},
                new Param() {Name = "Объем открытого интереса", NameInBase = "VolumeOpenInterest"}
            };
            this.mode = mode;
            if (mode == "add")
            {
                this.Title = "Добавить событие";
                this.mainEvent = new MainEvent();
            }
            else
            {
                this.Title = "Изменить событие";
                this.mainEvent = mainEvent;
                tbTitle.Text = mainEvent.Title;
                tbParam.Text = mainEvent.ValueParam.ToString();
                cmbParam.SelectedItem = listParam.FirstOrDefault(c => c.Name == mainEvent.MainParam.Name);
                RadioButton rd;
                foreach (Object child in ((Grid)grCondition.Content).Children)
                {
                    if (child is RadioButton)
                    {
                        rd = ((RadioButton)child);
                        rd.IsChecked = (mainEvent.Sign == rd.Content.ToString()) ? true : false;
                    }
                }
            }
            cmbParam.ItemsSource = listParam;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            int res = CheckForm();
            if (res == 0)
            {
                mainEvent.Title = tbTitle.Text;
                mainEvent.Sign = pressed.Content.ToString();
                mainEvent.MainParam = (Param)cmbParam.SelectedItem;
                mainEvent.ValueParam = double.Parse(tbParam.Text);
                if (mode == "add")
                {
                    parent.AddEvent(mainEvent);
                }
                else
                {
                    parent.EditEvent();         
                }
                this.Close();
            }
        }
        public int CheckForm() // возвращаем количество ошибок
        {
            bool check = true;
            int i = 0;
            check = ErrorControls.NotEmptyTextBox(tbTitle);
            ErrorControls.CountErrors(ref i, check);

            check = ErrorControls.EmptyComboBox(cmbParam);
            ErrorControls.CountErrors(ref i, check);

            check = (pressed.Content.ToString() != "");
            ErrorControls.CountErrors(ref i, check);

            check = ErrorControls.TextBoxToDouble(tbParam);
            ErrorControls.CountErrors(ref i, check);

            return i;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            parent = (RunWindow)this.Owner;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            pressed = (RadioButton)sender;
        }
    }
}
