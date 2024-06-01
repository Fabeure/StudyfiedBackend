using FluentAssertions;
using StudyfiedBackend.Models;
using StudyfiedBackendUnitTests.Helpers.ServiceInterfaces.FlashCards;
using StudyfiedBackendUnitTests.Mock.DataLayer;
using System.Diagnostics;

namespace StudyfiedBackendUnitTests.Tests.Controller
{
    [Collection("Database collection")]
    public class FlashCardsControllerTests : IDisposable
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
                FlashCard flashCard = service.generateFlashCard(randomTopic, i+1);
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
            
            FlashCard flashCard = service.generateFlashCard(topic, numberOfFlashCards);

            service.persistFlashCard(flashCard);

            FlashCard addedFlashCard = service.getAllFlashCards().First();

            //Act
            bool? result = service.deleteFlashCard(addedFlashCard.Id);

            //Assert
            service.getAllFlashCards().Should().BeEmpty();
            result.Should().BeTrue();
        }

        [Fact]
        public void BatchDeleteFlashCardTest()
        {
            //Arrange
            string topic = "history";
            int numberOfFlashCards = 3;

            for (int i=0; i<2; i++)
            {
                FlashCard flashCard = service.generateFlashCard(topic, numberOfFlashCards);
                service.persistFlashCard(flashCard);
            }

            List<FlashCard> addedFlashCards = service.getAllFlashCards();
            List<string> ids = addedFlashCards.Select(flashcard => flashcard.Id).ToList();

            //Act
            bool result = service.batchDeleteFlashCards(ids).Value;

            //Assert
            service.getAllFlashCards().Should().BeEmpty();
        }

        [Fact]
        public void UpdateFlashCardTest()
        {
            //Arrange
            string topic = "history";
            int numberOfFlashCards = 3;

            FlashCard flashCard = service.generateFlashCard(topic, numberOfFlashCards);

            service.persistFlashCard(flashCard);

            FlashCard addedFlashCard = service.getAllFlashCards().First();

            //Act 
            addedFlashCard.userId = "this is a test";

            service.updateFlashCard(flashCardToUpdate: addedFlashCard);

            FlashCard updatedFlashCard = service.getAllFlashCards().First();

            updatedFlashCard.userId.Should().Be(addedFlashCard.userId);

        }

        public void Dispose()
        {
            fakeClientWithInMemoryDataLayer.Dispose();
        }
    }
}
