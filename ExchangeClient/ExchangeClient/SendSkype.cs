using SKYPE4COMLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeClient
{
    public class SendSkype
    {
        private Skype skype_machine;

        public SendSkype(string login, string message)
        {
            Task t = SendMessageAsync(login, message);
        }
        public async Task SendMessageAsync(string loginSkype, string message)
        {
            string result = await Task.Run(() =>
            {
                try
                {
                    skype_machine = new Skype();
                    if (!skype_machine.Client.IsRunning)
                    {
                        skype_machine.Client.Start(true, true);
                    }
                    skype_machine.Client.OpenAddContactDialog(loginSkype);
                    skype_machine.SendMessage(loginSkype, message);
                    skype_machine.Client.Minimize();
                    result = "Сообщение отправлено";
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }

                return result;
            });
        }
    }
}
