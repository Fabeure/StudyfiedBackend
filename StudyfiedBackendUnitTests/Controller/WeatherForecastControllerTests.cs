using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using StudyfiedBackend.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyfiedBackendUnitTests.Controller
{
    public class WeatherForecastControllerTests
    {
        private readonly ILogger<WeatherForecastController> _weatherForecastLogger;
        public WeatherForecastControllerTests() {
            _weatherForecastLogger = A.Fake<ILogger<WeatherForecastController>>();
        }

        [Fact]
        public void getWeatherForecastTest()
        {
            //Arrange
            var controller = new WeatherForecastController(_weatherForecastLogger);

            //Act
            var result = controller.Get();

            //Assert
            var test = 1;
        }
    }
}
