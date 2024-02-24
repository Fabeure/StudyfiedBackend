using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
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
            var expectedResult = new List<Object>
            {
                new { Name = "Saber", Age = 21 },
                new { Name = "Farah", Age = 20 },
                new { Name = "Mohamed", Age = 22 },
                new { Name = "Abdelhak", Age = 21 },
                new { Name = "Zied", Age = 22 }
            };
            //Act
            var result = controller.Get();

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var actualStudents = ((OkObjectResult)result).Value as List<Object>;

            actualStudents.Should().NotBeNull();
            actualStudents.Should().BeEquivalentTo(expectedResult);
        }
    }
}
