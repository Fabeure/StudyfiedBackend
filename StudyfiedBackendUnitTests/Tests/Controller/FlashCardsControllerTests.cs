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
        public void getFlashCardTest()
        {
            string randomTopic = "gorillas";
            int numberOfFlashCards = 5; // we will check that 1,2,3,4 and 5 flashcards can be succesfully generated

            for (int i=0; i<numberOfFlashCards; i++)
            {
                FlashCard flashCard = service.getFlashCard(randomTopic, i+1);
                flashCard.Should().NotBeNull();
                flashCard.items.Count().Should().Be(i+1);
                flashCard.items.Keys.Should().NotContainNulls();
                flashCard.items.Keys.Should().AllSatisfy(ques => ques.Should().NotBeNullOrWhiteSpace().And.NotBeNullOrEmpty());
                flashCard.items.Values.Should().AllSatisfy(ans => ans.Should().NotBeNullOrWhiteSpace().And.NotBeNullOrEmpty());
            }
        }

        [Fact]
        public void DeleteFlashCardTest()
        {
            //Arrange
            string topic = "history";
            int numberOfFlashCards = 3;
            
            FlashCard flashCard = service.getFlashCard(topic, numberOfFlashCards);
            flashCard.Id = "6610720df900c331c96abd76";
            service.persistFlashCard(flashCard);
            List<FlashCard> test = service.getAllFlashCards();  
            FlashCard fetchedFlashCard = service.getExistingFlashCard(flashCard.Id);

            //Act
            bool? result = service.deleteFlashCard(fetchedFlashCard.Id);

            //Assert
            result.Should().BeTrue();

           
        }
    }
}
