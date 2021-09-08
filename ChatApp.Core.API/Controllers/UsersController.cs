using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<UsersController> _logger;
        private readonly IHubContext<ChatHub> _hubContext;

        public UsersController(ILogger<UsersController> logger, IHubContext<ChatHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("all")]
        public IEnumerable<UserDto> GetAll()
        {
            return Enumerable.Range(1, 1).Select(i => new UserDto
            {
                UserId = "abc",
                UserName = "Sam",
            })
            .ToArray();
        }

        [HttpGet("{userId}")]
        public UserDto GetUser(string userId)
        {
            return new UserDto
            {
                UserId = userId,
                UserName = "Sam",
            };
        }

        // ToDo:
        // HttpPost("register")
    }
}
