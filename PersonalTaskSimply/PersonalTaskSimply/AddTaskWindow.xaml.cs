using DbLibrary;
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

namespace PersonalTaskSimply
{
    /// <summary>
    /// Логика взаимодействия для AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : Window
    {
        private MainWindow parent;
        private string mode;
        private List<Employees> listEmployees;
        private DbLibrary.Task task;

        public AddTaskWindow(string mode = "add", DbLibrary.Task task = null)
        {
            InitializeComponent();
            this.mode = mode;
            if (mode == "add")
            {
                this.Title = "Добавить задачу";
                this.task = new DbLibrary.Task();
            }
            else
            {
                this.task = task;
                this.Title = "Редактирование данные";
                tbTitle.Text = task.Title;
                tbDateBegin.Text = task.DateBegin.ToString();
                tbDateEnd.Text = task.DateEnd.ToString();
                tbCustomer.Text = task.Customer;
                tbPhoneCustomer.Text = task.PhoneCustomer;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            int res = CheckUser();
            if (res == 0)
            {
                task.Title = tbTitle.Text;
                task.DateBegin = DateTime.Parse(tbDateBegin.Text);
                task.DateEnd = DateTime.Parse(tbDateEnd.Text);
                task.Customer = tbCustomer.Text;
                task.PhoneCustomer = tbPhoneCustomer.Text;
                if (mode == "edit")
                {
                    task.Employees.Clear();
                }
                for (int i = 0; i < lbEmployees.SelectedItems.Count; i++)
                {
                    task.Employees.Add((Employees)lbEmployees.SelectedItems[i]);
                }
                bool result;
                if (mode == "add")
                {
                    if (result = parent.DbLink.AddTask(task))
                    {
                        parent.AddTaskList(task);
                    }
                }
                else
                {
                    if (result = parent.DbLink.EditTask(task))
                    {
                        parent.UpdateTaskList();
                    }
                }
                if (result)
                {
                    this.Close();
                }
            }
        }
        private int CheckUser()
        {
            bool check;
            int i = 0;
            check = ErrorControls.NotEmptyTextBox(tbTitle);
            ErrorControls.CountErrors(ref i, check);

            check = ErrorControls.NotEmptyDataPicker(tbDateBegin);
            ErrorControls.CountErrors(ref i, check);

            check = ErrorControls.NotEmptyDataPicker(tbDateEnd);
            ErrorControls.CountErrors(ref i, check);

            check = ErrorControls.NotEmptyTextBox(tbCustomer);
            ErrorControls.CountErrors(ref i, check);

            if (lbEmployees.SelectedItems.Count == 0)
            {
                check = false;
                lbEmployees.Background = Brushes.Red;
            }
            else
            {
                lbEmployees.Background = (LinearGradientBrush)lbEmployees.FindResource("LightBrush");
            }
            ErrorControls.CountErrors(ref i, check);

            return i;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            parent = (MainWindow)this.Owner;
            listEmployees = parent.DbLink.GetAllPerson();
            lbEmployees.ItemsSource = listEmployees;
            if (mode == "edit")
            {
                lbEmployees.SelectAll();
                foreach (Employees item in task.Employees)
                {

                }
            }
        }
    }
}
