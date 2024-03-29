using FakeItEasy;
using FluentAssertions;
using StudyfiedBackend.Controllers.FlashCards;

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
            var result = controller.get(topic);

            //Assert
            var actualResult = result.ResultItem;

            actualResult.Should().NotBeNull();
            actualResult.Should().BeEquivalentTo("");
        }
    }
}
