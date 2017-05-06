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
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private MainWindow parent;

        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            parent = (MainWindow)this.Owner;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            int res = CheckUser();
            if (res == 0)
            {
                Accounts account = new Accounts();
                account.Login = tbLogin.Text;
                account.LoginSkype = tbLoginSkype.Text;
                account.Password = tbPass.Password;
                account.Email = tbEmail.Text;

                Message message = new Message();
                message.Title = "register";
                message.Account = account;
                message.Result = null;


                Message recieveMessage = parent.SendToServer(message);

                if (recieveMessage != null && recieveMessage.Result != null)
                {
                    if (recieveMessage.Result.Code == 0)
                    {   // нет ошибок, данные вставлены в базу можно загружать основное окно
                        this.Close();
                        parent.Hide();
                        RunWindow run = new RunWindow();
                        parent.MyAccount = account;
                        run.Owner = parent;
                        run.Show();
                    }
                    else
                    {
                        MessageBox.Show(recieveMessage.Result.Code + " " + recieveMessage.Result.TitleError);
                    }
                }
                else
                {
                    MessageBox.Show("Нет соединения с сервером");
                }
            }
        }

        private int CheckUser()
        {
            bool check;
            int i = 0;
            check = ErrorControls.NotEmptyTextBox(tbLogin) &&
                ErrorControls.RegexTextBox(tbLogin, "^[a-z]+$");
            ErrorControls.CountErrors(ref i, check);

            check = ErrorControls.NotEmptyTextBox(tbLoginSkype) &&
                ErrorControls.RegexTextBox(tbLoginSkype, "^[a-z]+$");
            ErrorControls.CountErrors(ref i, check);

            check = ErrorControls.NotEmptyTextBox(tbEmail) &&
                ErrorControls.RegexTextBox(tbEmail, "^[0-9a-z_\\.-]+@[0-9a-z_\\.-]+\\.[a-z]{2,}?$");
            ErrorControls.CountErrors(ref i, check);

            check = ErrorControls.NotEmptyPasswordBox(tbPass) &&
                ErrorControls.NotEmptyPasswordBox(tbRepeatPass) &&
                (tbPass.Password == tbRepeatPass.Password);
            if (!check)
            {
                tbPass.Background = Brushes.Red;
                tbRepeatPass.Background = Brushes.Red;
            }
            ErrorControls.CountErrors(ref i, check);

            return i;
        }

    }
}
