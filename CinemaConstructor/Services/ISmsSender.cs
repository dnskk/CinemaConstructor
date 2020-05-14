using System.Threading.Tasks;

namespace CinemaConstructor.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
