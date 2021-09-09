using System;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Core.API.Database.Entities;
using ChatApp.Core.API.Database.Repositories.Interfaces;
using ChatApp.Core.API.Hubs.Interfaces;
using ChatApp.Core.API.Managers;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        private readonly static ConnectionMapping<string> _connections =
            new ConnectionMapping<string>();

        private readonly IUserRepository _userRepository;
        private readonly IConnectionRepository _connectionRepository;

        public ChatHub(IUserRepository userRepository, IConnectionRepository connectionRepository)
        {
            _userRepository = userRepository;
            _connectionRepository = connectionRepository;
        }

        public async Task SendMessage(string to, string from, string message)
        {
            foreach (var connectionId in _connections.GetConnections(to))
            {
                await Clients.Client(connectionId).ReceivePrivateMessage(from + ": " + message);
            }
        }

        public async Task SendBroadcastMessage(string from, string message)
        {
            var connections = _userRepository
                .GetAll()
                    .Include(x => x.Connections)
                .FirstOrDefault(x => x.Username == from)
                .Connections
                .Select(x => x.ConnectionId);

            await Clients.AllExcept(connections.ToList()).ReceivePrivateMessage($"{from} says: \"{message}\"!");
        }

        public override Task OnConnectedAsync()
        {
            string name = Context.GetHttpContext().Request.Query["username"];
            var userId = Guid.NewGuid().ToString();

            _connections.Add(name, Context.ConnectionId);

            var user = new User
            {
                UserId = userId,
                Username = name,
            };

            var connection = new Connection
            {
                UserId = userId,
                ConnectionId = Context.ConnectionId,
            };

            _userRepository.Add(user);
            _userRepository.SaveChangesAsync();

            _connectionRepository.Add(connection);
            _connectionRepository.SaveChangesAsync();

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