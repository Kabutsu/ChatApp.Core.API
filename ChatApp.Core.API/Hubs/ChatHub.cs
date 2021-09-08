using System;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Core.API.Hubs.Interfaces;
using ChatApp.Core.API.Managers;
using Microsoft.AspNetCore.SignalR;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        private readonly static ConnectionMapping<string> _connections =
            new ConnectionMapping<string>();

        public async Task SendMessage(string to, string from, string message)
        {
            foreach (var connectionId in _connections.GetConnections(to))
            {
                await Clients.Client(connectionId).ReceivePrivateMessage(from + ": " + message);
            }
        }

        public async Task SendBroadcastMessage(string from, string message)
        {
            await Clients.AllExcept(_connections.GetConnections(from).ToList()).ReceivePrivateMessage($"{from} says: \"{message}\"!");
        }

        public override Task OnConnectedAsync()
        {
            string name = Context.GetHttpContext().Request.Query["username"];

            _connections.Add(name, Context.ConnectionId);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            string name = Context.User.Identity.Name;

            //_connections.Remove(name, Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);
        }
    }
}