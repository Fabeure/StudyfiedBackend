using FakeItEasy;
using FluentAssertions;
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
        private readonly FakeServiceWithInMemoryDataLayer fakeClientWithInMemoryDataLayer;
        private readonly FlashCardServiceInterface service;
        public FlashCardsControllerTests(FakeServiceWithInMemoryDataLayer fakeClientWithInMemoryDataLayer)
        {
            this.fakeClientWithInMemoryDataLayer = fakeClientWithInMemoryDataLayer;
            service = new FlashCardServiceInterface(client: this.fakeClientWithInMemoryDataLayer.FakeHttpClient);
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

            FlashCard flashCard = new FlashCard(items: testData, userId: "salem ena user");

            //Act
            service.persistFlashCard(flashCardWithUserId: flashCard);
            List<FlashCard> flashcards = service.getAllFlashCards();

            //Assert
            flashcards.Should().NotBeEmpty();
            flashcards.Count.Should().Be(1);
            flashcards.First().items.Should().NotBeNull();
            flashcards.First().items.Should().Equal(testData);
        }

        [Fact]
        public void DeleteFlashCardTest()
        {
            //Arrange
            Dictionary<string,string> testdata = new Dictionary<string, string>()
            {
                { "question1", "answer1" },
                { "question2", "answer2" },
            };
        }
    }
}
