using FakeItEasy;
using FluentAssertions;
using StudyfiedBackend.Controllers.FlashCards;
using StudyfiedBackend.Models;
using StudyfiedBackendUnitTests.Helpers.ServiceInterfaces;
using StudyfiedBackendUnitTests.Helpers.ServiceInterfaces.FlashCards;
using StudyfiedBackendUnitTests.Mock.DataLayer;
using static System.Net.WebRequestMethods;

namespace StudyfiedBackendUnitTests.Tests.Controller
{
    [Collection("Database collection")]
    public class FlashCardsControllerTests
    {
        private readonly FakeClientWithInMemoryDataLayer _fakeClientWithInMemoryDataLayer;
        private readonly FlashCardServiceInterface _serviceInterface;
        public FlashCardsControllerTests(FakeClientWithInMemoryDataLayer fakeClientWithInMemoryDataLayer)
        {
            _fakeClientWithInMemoryDataLayer = fakeClientWithInMemoryDataLayer;
            _serviceInterface = new FlashCardServiceInterface(client: _fakeClientWithInMemoryDataLayer.FakeHttpClient);
        }

        [Fact]
        public void InsertAndGetAllFlashCardsTest()
        {
            //Arrange

            Dictionary<string, string> testData = new Dictionary<string, string>()
            {
                { "question1", "answer1" },
                { "question2", "answer2" },
            };

            FlashCard flashCard = new FlashCard(items: testData, userId:"salem ena user");

            //Act
            _serviceInterface.persistFlashCard(flashCardWithUserId: flashCard);
            List<FlashCard> flashcards = _serviceInterface.getAllFlashCards();

            //Assert
            flashcards.Should().NotBeEmpty();
            flashcards.Count.Should().Be(1);
            flashcards.First().items.Should().NotBeNull();
            flashcards.First().items.Should().Equal(testData);
        }
    }
}
