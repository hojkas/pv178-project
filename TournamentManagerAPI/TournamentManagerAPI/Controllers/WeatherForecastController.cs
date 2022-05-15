using Microsoft.AspNetCore.Mvc;
using TournamentManagerAPI.Data.Entities;
using TournamentManagerAPI.Data.Repositories;

namespace TournamentManagerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetPlayers")]
        public async Task<IEnumerable<Player>> Get()
        {
            return await PlayerRepository.GetPlayersAsync();
        }
    }
}