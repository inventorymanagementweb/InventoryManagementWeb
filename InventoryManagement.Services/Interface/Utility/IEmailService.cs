using System.Threading.Tasks;

namespace InventoryManagement.Services.Interface.Utility
{
    public interface IEmailService
    {
        Task SendSingleEmail(string toEmail, string subject, string htmlContent);
    }
}
