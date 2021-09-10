using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Core.API.Database.Repositories.Interfaces;
using ChatApp.Core.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SignalRChat.Hubs;

namespace ChatApp.Core.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IUserRepository _userRepository;

        public UsersController(ILogger<UsersController> logger, IHubContext<ChatHub> hubContext, IUserRepository userRepository)
        {
            _logger = logger;
            _hubContext = hubContext;
            _userRepository = userRepository;
        }

        [HttpGet("all")]
        public IEnumerable<UserDto> GetAll()
        {
            return _userRepository
                .GetAll()
                .Select(x => new UserDto
                {
                    Id = Guid.Parse(x.UserId),
                    Username = x.Username,
                })
                .ToList();
        }

        [HttpGet("{userId:guid}")]
        public async Task<UserDto> GetUser(Guid userId)
        {
            var user = await _userRepository.Get(userId.ToString());
            return new UserDto
            {
                Id = Guid.Parse(user.UserId),
                Username = user.Username,
            };
        }

        [HttpPost("register")]
        public async Task<UserDto> AddUser(UserDto user)
        {
            var newUser = await _userRepository.Add(new Database.Entities.User
            {
                UserId = user.Id.ToString(),
                Username = user.Username,
            });

            return new UserDto
            {
                Id = Guid.Parse(newUser.UserId),
                Username = newUser.Username,
            };
        }
    }
}
