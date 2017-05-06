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
    /// Логика взаимодействия для AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        private MainWindow parent;
        private string mode;
        private Employees person;

        public AddEmployeeWindow(string mode = "add", Employees person = null)
        {
            InitializeComponent();
            this.mode = mode;
            if (mode == "add")
            {
                this.Title = "Добавить служещего";
                this.person = new Employees();
            }
            else
            {
                this.person = person;
                this.Title = "Редактирование данные";
                tbName.Text = person.Name;
                tbLastName.Text = person.LastName;
                tbBirthDay.Text = person.DateBirth.ToString();
                tbAdress.Text = person.Adress;
                tbPhone.Text = person.Phone;
                tbPosition.Text = person.Position;
                tbSalary.Text = person.Salary.ToString();
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            int res = CheckUser();
            if (res == 0)
            {
                person.Name = tbName.Text;
                person.LastName = tbLastName.Text;
                person.DateBirth = DateTime.Parse(tbBirthDay.Text);
                person.Adress = tbAdress.Text;
                person.Phone = tbPhone.Text;
                person.Position = tbPosition.Text;
                person.Salary = float.Parse(tbSalary.Text);
                // пока показатели будем формировать рандомно 
                Random rnd = new Random();
                person.erk = rnd.Next(0, 100);
                person.pkp = rnd.Next(0, 100);
                person.ekr = rnd.Next(0, 100);
                person.ems = rnd.Next(0, 100);
                person.skr = rnd.Next(0, 100);
                person.GetRate();
                bool result;
                if (mode == "add")
                {
                    if (result = parent.DbLink.AddEmployee(person))
                    {
                        parent.AddPersonList(person);
                    }
                }
                else
                {
                    if (result = parent.DbLink.EditEmployee(person))
                    {
                        parent.UpdatePersonList();
                    }
                }
                if (result)
                {
                    this.Close();
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private int CheckUser()
        {
            bool check;
            int i = 0;
            check = ErrorControls.NotEmptyTextBox(tbName);
            ErrorControls.CountErrors(ref i, check);

            check = ErrorControls.NotEmptyTextBox(tbLastName);
            ErrorControls.CountErrors(ref i, check);

            check = ErrorControls.NotEmptyDataPicker(tbBirthDay);
            ErrorControls.CountErrors(ref i, check);

            check = ErrorControls.NotEmptyTextBox(tbAdress);
            ErrorControls.CountErrors(ref i, check);

            check = ErrorControls.NotEmptyTextBox(tbPosition);
            ErrorControls.CountErrors(ref i, check);

            check = ErrorControls.NotEmptyTextBox(tbSalary) &&
                ErrorControls.TextBoxToDouble(tbSalary);
            ErrorControls.CountErrors(ref i, check);

            return i;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            parent = (MainWindow)this.Owner;
        }
    }
}
