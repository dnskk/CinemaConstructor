using System.Threading.Tasks;

namespace AdminPanel.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
