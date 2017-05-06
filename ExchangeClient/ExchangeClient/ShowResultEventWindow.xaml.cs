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
    /// Логика взаимодействия для ShowResultEventWindow.xaml
    /// </summary>
    public partial class ShowResultEventWindow : Window
    {
        private RunWindow parent;
        public ShowResultEventWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            parent = (RunWindow)this.Owner;
            ListView myListView = new ListView();
            GridView myGridView = new GridView();
            myGridView.AllowsColumnReorder = true;
            AddGridColumn(myGridView, "Название ценной бумаги", "Title");
            AddGridColumn(myGridView, "Параметр", "MainParam");
            AddGridColumn(myGridView, "Условие ", "Sign");
            AddGridColumn(myGridView, "Сигнальное значение параметра", "ValueParam");
            myListView.HorizontalAlignment = HorizontalAlignment.Center;
            myListView.BorderBrush = Brushes.White;
            myListView.ItemsSource = parent.ListResultEvents;
            myListView.View = myGridView;
            myListView.SetValue(Grid.RowProperty, 0);
            myListView.SetValue(Grid.ColumnProperty, 1);
            myGrid.Children.Add(myListView);
        }

        public void AddGridColumn(GridView myGridView, string header, string name)
        {
            GridViewColumn gvc1 = new GridViewColumn();
            gvc1.DisplayMemberBinding = new Binding(name);
            gvc1.Header = header;
            gvc1.Width = 150;
            myGridView.Columns.Add(gvc1);
        }
    }
}
