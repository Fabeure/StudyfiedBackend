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
            new { Name = "Alice", Age = 20 },
            new { Name = "Bob", Age = 22 },
            new { Name = "Charlie", Age = 21 }
        };
            return Ok(students);
        }
    }
}
