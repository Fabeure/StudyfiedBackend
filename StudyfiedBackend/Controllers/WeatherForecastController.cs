using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using System.Text;

namespace StudyfiedBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly bool weather = true;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get()
        {
            // Dummy data for demonstration purposes
            var students = new List<object>
        {
            new { Name = "Saber", Age = 20 },
            new { Name = "Farah", Age = 22 },
            new { Name = "Mohamed", Age = 22 },
            new { Name = "Abdelhak", Age = 21}
        };
            return Ok(students);
        }
    }
}
