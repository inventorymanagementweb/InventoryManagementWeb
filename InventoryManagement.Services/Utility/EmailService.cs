using System;
using System.Threading.Tasks;
using EASendMail;
using InventoryManagement.Commons;
using InventoryManagement.Repositories.Interface.Infastructure;
using InventoryManagement.Services.Infastructure;
using InventoryManagement.Services.Interface.Utility;

namespace InventoryManagement.Services.Utility
{
    public class EmailService : BaseService, IEmailService
    {
        public EmailService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task SendSingleEmail(string toEmail, string subject, string htmlContent)
        {
            try
            {
                Logger.Info($"EmailService.SendSingleEmail(string toEmail, string subject, string htmlContent); toEmail: {toEmail}, subject: {subject}, htmlContent {htmlContent}");

                var oMail = new SmtpMail(Constants.ConfigurationEmailServiceLicenseCode);
                var oSmtp = new SmtpClient();

                oMail.Sender = Constants.ConfigurationEmailServiceDisplayName;

                oMail.From = Constants.ConfigurationEmailServiceDisplayName;

                oMail.To = toEmail;

                oMail.Subject = subject;

                oMail.HtmlBody = htmlContent;

                var oServer = new SmtpServer(Constants.ConfigurationEmailServiceSmtpServer)
                {
                    Port = 587,
                    ConnectType = SmtpConnectType.ConnectSSLAuto,
                    User = Constants.ConfigurationEmailServiceEmailAccount,
                    Password = Constants.ConfigurationEmailServiceEmailPassword
                };

                oSmtp.SendMail(oServer, oMail);

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return Task.FromException(ex);
            }
        }
    }
}
