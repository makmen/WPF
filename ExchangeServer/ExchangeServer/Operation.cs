using Base;
using ExchangeServer.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExchangeServer
{
    public interface IExecute
    {
        void Execute(Message message, ServerObject server, string id);
    }

    public class Register : IExecute
    {
        public void Execute(Message message, ServerObject server, string id)
        {
            if (message.Account == null)
            {
                message.Account = new Accounts();
            }
            if (CheckUser(message))
            { 
                // вставляем в базу данных 
                Accounts newAccount = new Accounts();
                newAccount.Login = message.Account.Login;
                newAccount.LoginSkype = message.Account.LoginSkype;
                newAccount.Email = message.Account.Email;
                newAccount.Password = message.Account.Password;
                newAccount.DateRegister = DateTime.Now;
                message.Result = new Result();
                if (server.Connect.AddAccount(newAccount))
                {
                    message.Result.Code = 0;
                }
                else
                {
                    message.Result.Code = 2;
                    message.Result.TitleError = "Ошибка работы с базой данных";
                }
            }
        }

        public bool CheckUser(Message message)
        {
            bool check = (message.Account.Login != "") &&
                Regex.IsMatch(message.Account.Login, "^[a-z]+$");
            if (!check)
            {
                message.Result.Code = 1;
                message.Result.TitleError = "В поле Login были допущены ошибки";
                return check;
            }

            check = (message.Account.LoginSkype != "") &&
                Regex.IsMatch(message.Account.LoginSkype, "^[a-z]+$");
            if (!check)
            {
                message.Result.Code = 1;
                message.Result.TitleError = "В поле LoginSkype были допущены ошибки";
                return check;
            }

            check = (message.Account.Email != "") &&
                Regex.IsMatch(message.Account.Email, "^[0-9a-z_\\.-]+@[0-9a-z_\\.-]+\\.[a-z]{2,}?$");
            if (!check)
            {
                message.Result.Code = 1;
                message.Result.TitleError = "В поле Email были допущены ошибки";
                return check;
            }

            check = (message.Account.Password != "");
            if (!check)
            {
                message.Result.Code = 1;
                message.Result.TitleError = "В поле Password были допущены ошибки";
                return check;
            }

            return check;
        }

    }

    public class CheckIn : IExecute
    {
        public void Execute(Message message, ServerObject server, string id)
        {
            if (CheckData(message))
            {
                Accounts temp = server.Connect.GetAccount(message.Account.Login, message.Account.Password);
                message.Result = new Result();
                if (temp != null)
                {
                    message.Result.Code = 0;
                    message.Account.LoginSkype = temp.LoginSkype;
                    message.Account.Email = temp.Email;
                    message.Account.Id = temp.Id;
                    message.Account.DateRegister = temp.DateRegister;
                }
                else
                {
                    message.Result.Code = 3;
                    message.Result.TitleError = "Не верный логин или пароль";
                }
            }
        }

        public bool CheckData(Message message)
        {
            bool check = (message.Account.Login != "") &&
                Regex.IsMatch(message.Account.Login, "^[a-z]+$");
            if (!check)
            {
                message.Result.Code = 1;
                message.Result.TitleError = "В поле Login были допущены ошибки";
                return check;
            }

            check = (message.Account.Password != "");
            if (!check)
            {
                message.Result.Code = 1;
                message.Result.TitleError = "В поле Password были допущены ошибки";
                return check;
            }

            return check;
        }
    }

    public class Exit : IExecute
    {
        public void Execute(Message message, ServerObject server, string id)
        {
            server.RemoveConnection(id);
        }
    }

    public class KeepEya : IExecute
    {
        public void Execute(Message message, ServerObject server, string id)
        {
            message.Result = new Result();
            if (message.AllEvents != null)
            {
                for (int index = 0; index < message.AllEvents.Length; index++)
                {
                    // проверяем есть ли вообще такая ценная бумага
                    Parameters parameter = server.Connect.GetParameter(message.AllEvents[index].Title);
                    if (parameter != null) // обрабатываем события
                    {
                        PrepareAnswer(message.AllEvents[index], parameter);
                        message.AllEvents[index].EventError = "";
                    }
                    else
                    {
                        message.AllEvents[index].EventDone = false;
                        message.AllEvents[index].EventError = "Нет такой ценной бумаги";
                    }
                }
                message.Result.Code = 0;
            }
        }

        public void PrepareAnswer(MainEvent mainEvent, Parameters parameter)
        {
            double value = double.Parse((parameter.GetType().GetProperty(mainEvent.MainParam.NameInBase).GetValue(parameter)).ToString());
            if (ComparisonEvent(value, mainEvent.ValueParam, mainEvent.Sign))
            {
                mainEvent.EventDone = true;
            }
            else
            {
                mainEvent.EventDone = false;
            }
        }

        public bool ComparisonEvent(double valueBase, double need, string sign)
        {
            bool res = false;
            switch(sign)
            {
                case ">":
                    if (valueBase > need)
                    {
                        res = true;
                    }
                    break;
                case "<":
                    if (valueBase < need)
                    {
                        res = true;
                    }
                    break;
                case "<=":
                    if (valueBase <= need)
                    {
                        res = true;
                    }
                    break;
                case ">=":
                    if (valueBase >= need)
                    {
                        res = true;
                    }
                    break;
                case "=":
                    if (valueBase == need)
                    {
                        res = true;
                    }
                    break;
            }

            return res;
        }
    }

    public class Operation
    {
        private IExecute execute;
        private ServerObject server;
        private Message message;

        public Operation(Message message, ServerObject server)
        {
            this.message = message;
            this.server = server;
            switch (message.Title)
            {
                case "register":
                    SetOperation(new Register());
                    break;
                case "checkin":
                    SetOperation(new CheckIn());
                    break;
                case "keepeya":
                    SetOperation(new KeepEya());
                    break;
                default: // exit либо еще что
                    SetOperation(new Exit());
                    break;
            }
        }

        public void Execute(string id)
        {
            if (execute != null)
            {
                execute.Execute(message, server, id);
            }
        }

        public void SetOperation(IExecute execute)
        {
            this.execute = execute;
        }
    }
}
