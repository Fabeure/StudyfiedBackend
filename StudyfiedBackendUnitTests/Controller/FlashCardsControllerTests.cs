using DotnetGeminiSDK.Client.Interfaces;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudyfiedBackend.Controllers;
using StudyfiedBackend.Controllers.FlashCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyfiedBackendUnitTests.Controller
{
    public class FlashCardsControllerTests
    {
        private readonly IGeminiClient _geminiClient;

        public FlashCardsControllerTests()
        {
            _geminiClient = A.Fake<IGeminiClient>(); 
        }

        [Fact]
        public void getFlashCardsTest()
        {
            //Arrange
            var controller = new FlashCardsController(_geminiClient);

            var topic = "Chickens";
            var expectedResultRegex = "";
            //Act
            var result = controller.getFlashCardResponse(topic);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var actualStudents = ((OkObjectResult)result.Result).Value as List<Object>;

            actualStudents.Should().NotBeNull();
            actualStudents.Should().BeEquivalentTo("");
        }
    }
}
