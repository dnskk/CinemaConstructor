using System.Threading.Tasks;

namespace AdminPanel.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
