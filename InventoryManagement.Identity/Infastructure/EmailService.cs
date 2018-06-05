using System;
using System.Threading.Tasks;
using EASendMail;
using Microsoft.AspNet.Identity;

namespace InventoryManagement.Identity.Infastructure
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await SemdSingleEmail(message.Destination, message.Subject, message.Body, string.Empty);
        }

        public Task SemdSingleEmail(string toEmail, string subject, string plainTextContent, string htmlContent)
        {
            try
            {
                var oMail = new SmtpMail(Commons.Constants.ConfigurationEmailServiceLicenseCode);
                var oSmtp = new SmtpClient();

                oMail.Sender = Commons.Constants.ConfigurationEmailServiceDisplayName;

                oMail.From = Commons.Constants.ConfigurationEmailServiceDisplayName;

                oMail.To = toEmail;

                oMail.Subject = subject;

                oMail.HtmlBody = plainTextContent;

                var oServer = new SmtpServer(Commons.Constants.ConfigurationEmailServiceSmtpServer)
                {
                    Port = 587,
                    ConnectType = SmtpConnectType.ConnectSSLAuto,
                    User = Commons.Constants.ConfigurationEmailServiceEmailAccount,
                    Password = Commons.Constants.ConfigurationEmailServiceEmailPassword
                };

                oSmtp.SendMail(oServer, oMail);

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                return Task.FromException(ex);
            }
        }
    }
}
