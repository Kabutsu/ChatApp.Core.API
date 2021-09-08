using System.Threading.Tasks;

namespace ChatApp.Core.API.Hubs.Interfaces
{
    public interface IChatClient
    {
        Task ReceiveMessage(string user, string message);
        Task ReceivePrivateMessage(string message);
    }
}
