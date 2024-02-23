using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;

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
            new { Name = "Saber", Age = 21 },
            new { Name = "Farah", Age = 20 },
            new { Name = "Mohamed", Age = 22 },
            new { Name = "Abdelhak", Age = 21},
            new { Name = "Zied", Age = 22}
        };
            return Ok(students);
        }
    }
}
