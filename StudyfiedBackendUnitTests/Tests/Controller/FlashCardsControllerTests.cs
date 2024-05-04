using FakeItEasy;
using FluentAssertions;
using StudyfiedBackend.Controllers.FlashCards;
using StudyfiedBackend.Models;
using StudyfiedBackendUnitTests.Mock.DataLayer;
using static System.Net.WebRequestMethods;

namespace StudyfiedBackendUnitTests.Tests.Controller
{
    [Collection("Database collection")]
    public class FlashCardsControllerTests
    {
        private readonly IFlashCardsService _flashCardsService;
        private readonly FakeClientWithInMemoryDataLayer _fakeClientWithInMemoryDataLayer;

        public FlashCardsControllerTests(FakeClientWithInMemoryDataLayer fakeClientWithInMemoryDataLayer)
        {
            _flashCardsService = A.Fake<IFlashCardsService>();
            _fakeClientWithInMemoryDataLayer = fakeClientWithInMemoryDataLayer;
        }

        [Fact]
        public async void PersistFlashCardTest()
        {
            //Arrange
            var client = _fakeClientWithInMemoryDataLayer.FakeHttpClient;
            var db = _fakeClientWithInMemoryDataLayer.Database;

            var flashCardsCollection = db.GetCollection<FlashCard>("FlashCards");

            Dictionary<string, string> testData = new Dictionary<string, string>()
            {
                { "question1", "answer1" },
                { "question2", "answer2"}
            };

            FlashCard flashCard = new FlashCard(items: testData);

            flashCardsCollection.InsertOne(flashCard);

            var test = client.GetAsync("/api/FlashCards/getExistingFlashCard?id=100").Result;


            //Act

            //Assert
        }
    }
}
