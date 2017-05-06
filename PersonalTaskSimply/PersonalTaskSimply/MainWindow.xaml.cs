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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PersonalTaskSimply
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DbConnect dbLink;
        private List<Employees> listEmployees;
        private List<DbLibrary.Task> listTask;
        private List<KeyValuePair<string, int>> diagrammList;

        public DbConnect DbLink
        {
            get { return dbLink; }
        }

        public MainWindow()
        {
            InitializeComponent();
            dbLink = new DbConnect();
            listEmployees = dbLink.GetAllPerson();
            lbPersons.ItemsSource = listEmployees;
            listTask = dbLink.GetAllTask();
            lbTasks.ItemsSource = listTask;
            diagrammList = new List<KeyValuePair<string, int>>();
            diagrammList.Add(new KeyValuePair<string, int>("erk", 0));
            diagrammList.Add(new KeyValuePair<string, int>("pkp", 0));
            diagrammList.Add(new KeyValuePair<string, int>("ekr", 0));
            diagrammList.Add(new KeyValuePair<string, int>("ems", 0));
            diagrammList.Add(new KeyValuePair<string, int>("skr", 0));
            ColumnSeries.DataContext = diagrammList;
            lbRate.Content = "0";
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeWindow addEmployee = new AddEmployeeWindow();
            addEmployee.Owner = this;
            addEmployee.Show();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lbPersons.SelectedIndex >= 0)
            {
                AddEmployeeWindow editEmployee = new AddEmployeeWindow("edit", (Employees)lbPersons.SelectedItem);
                editEmployee.Owner = this;
                editEmployee.Show();
            }
            else
            {
                MessageBox.Show("Choose a person");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lbPersons.SelectedIndex >= 0)
            {
                Employees person = (Employees)lbPersons.SelectedItem;
                MessageBoxResult res = MessageBox.Show("Do you realy want to delete this Person: " + person.ToString(), "Delete person", MessageBoxButton.OKCancel);
                if (res == MessageBoxResult.OK)
                {
                    DbLink.DeleteEmployee(person);
                    listEmployees.Remove(person);
                    UpdatePersonList();
                }
            }
            else
            {
                MessageBox.Show("Choose a person");
            }
        }
        
        private void lbPersons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbPersons.SelectedIndex >= 0)
            {
                Employees person = (Employees)lbPersons.SelectedItem;
                tbPersons.Text = person.ForTextBoxToString();
                ColumnSeries.DataContext = null;
                diagrammList.Clear();
                diagrammList.Add(new KeyValuePair<string, int>("erk", (int)person.erk));
                diagrammList.Add(new KeyValuePair<string, int>("pkp", (int)person.pkp));
                diagrammList.Add(new KeyValuePair<string, int>("ekr", (int)person.ekr));
                diagrammList.Add(new KeyValuePair<string, int>("ems", (int)person.ems));
                diagrammList.Add(new KeyValuePair<string, int>("skr", (int)person.skr));
                ColumnSeries.DataContext = diagrammList;
                lbRate.Content = person.Rate.ToString();
            }
        }

        public void AddPersonList(Employees person)
        {
            listEmployees.Add(person);
            lbPersons.ItemsSource = null;
            lbPersons.ItemsSource = listEmployees;
            tbPersons.Text = "";
        }

        public void AddTaskList(DbLibrary.Task task)
        {
            listTask.Add(task);
            lbTasks.ItemsSource = null;
            lbTasks.ItemsSource = listTask;
            tbTasks.Text = "";
        }

        public void UpdatePersonList()
        {
            lbPersons.ItemsSource = null;
            lbPersons.ItemsSource = listEmployees;
            tbPersons.Text = "";
        }

        public void UpdateTaskList()
        {
            lbTasks.ItemsSource = null;
            lbTasks.ItemsSource = listTask;
            tbTasks.Text = "";
        }

        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            AddTaskWindow addTask = new AddTaskWindow();
            addTask.Owner = this;
            addTask.Show();
        }

        private void btnEditTask_Click(object sender, RoutedEventArgs e)
        {
            if (lbTasks.SelectedIndex >= 0)
            {
                AddTaskWindow editTask = new AddTaskWindow("edit", (DbLibrary.Task)lbTasks.SelectedItem);
                editTask.Owner = this;
                editTask.Show();
            }
            else
            {
                MessageBox.Show("Choose a task");
            }
        }

        private void btnDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (lbTasks.SelectedIndex >= 0)
            {
                DbLibrary.Task task = (DbLibrary.Task)lbTasks.SelectedItem;
                MessageBoxResult res = MessageBox.Show("Do you realy want to delete this Task: " + task.ToString(), "Delete task", MessageBoxButton.OKCancel);
                if (res == MessageBoxResult.OK)
                {
                    DbLink.DeleteEmployee(task);
                    listTask.Remove(task);
                    UpdateTaskList();
                }
            }
            else
            {
                MessageBox.Show("Choose a task");
            }
        }

        private void lbTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbTasks.SelectedIndex >= 0)
            {
                DbLibrary.Task task = (DbLibrary.Task)lbTasks.SelectedItem;
                //tbTasks.Text = task.ForTextBoxToString();
            }
        }
    }
}
