using AM.BLL.Common.Core;
using AM.DM.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace AM.BLL.Common.Infrastructure
{
    public class EmailHandlerService : IEmailHandlerService
    {
        private readonly IGlobalConfigurationService _IGlobalConfigurationService;
        public EmailHandlerService(IGlobalConfigurationService globalConfigurationService)
        {
            _IGlobalConfigurationService = globalConfigurationService;
        }

        public void SendEmail(string pToEmail, string pSubject, string pBody)
        {
            GlobalConfigModel emailConfig = _IGlobalConfigurationService.GetMyConfiguration();

            using (var message = new MailMessage())
            {
                message.To.Add(new MailAddress(pToEmail));
                message.From = new MailAddress(emailConfig.EmailConfig.FromAddress);
                //message.CC.Add(new MailAddress("cc@email.com"));
                //message.Bcc.Add(new MailAddress("bcc@email.com"));
                message.Subject = pSubject;
                message.Body = pBody;
                message.IsBodyHtml = true;

                using (var client = new SmtpClient(emailConfig.EmailConfig.Server))
                {
                    client.Port = Convert.ToInt16(emailConfig.EmailConfig.Port);
                    client.Credentials = new NetworkCredential(emailConfig.EmailConfig.FromAddress, emailConfig.EmailConfig.MailPassword);
                    client.EnableSsl = true;
                    client.Send(message);
                }
            }
        }

    }

}
