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
        private readonly IFlashCardsService _flashCardsService;

        public FlashCardsControllerTests()
        {
            _flashCardsService = A.Fake<IFlashCardsService>(); 
        }

        [Fact]
        public void getFlashCardsTest()
        {
            //Arrange
            var controller = new FlashCardsController(_flashCardsService);

            var topic = "Chickens";
            var expectedResultRegex = "";
            //Act
            var result = controller.getFlashCards(topic);

            //Assert
            var actualResult = result.ResultItem;

            actualResult.Should().NotBeNull();
            actualResult.Should().BeEquivalentTo("");
        }
    }
}
