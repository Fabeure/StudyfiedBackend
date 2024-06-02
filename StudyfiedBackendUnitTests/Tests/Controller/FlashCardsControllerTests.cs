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
            // Scenario: Insert flash card and retrieve all flash cards
            // Step 1: Arrange - Prepare test data and a new flash card
            Dictionary<string, string> testData = new Dictionary<string, string>()
    {
        { "question1", "answer1" },
        { "question2", "answer2" },
    };
            FlashCard flashCard = new FlashCard(items: testData, userId: "salem ena user");

            // Step 2: Act - Insert the flash card and retrieve all flash cards
            service.persistFlashCard(flashCardWithUserId: flashCard);
            List<FlashCard> flashcards = service.getAllFlashCards();

            // Step 3: Assert - Verify the flash card is inserted and retrieved correctly
            flashcards.Should().NotBeEmpty();
            flashcards.Count.Should().Be(1);
            flashcards.First().items.Should().NotBeNull();
            flashcards.First().items.Should().Equal(testData);
        }

        [Fact]
        public void getFlashCardTest()
        {
            // Scenario: Generate flash cards
            // Step 1: Arrange - Define topic and number of flash cards to generate
            string randomTopic = "gorillas";
            int numberOfFlashCards = 5;

            // Step 2: Act - Generate flash cards
            for (int i = 0; i < numberOfFlashCards; i++)
            {
                FlashCard flashCard = service.generateFlashCard(randomTopic, i + 1);

                // Step 3: Assert - Verify each generated flash card
                flashCard.Should().NotBeNull();
                flashCard.items.Count().Should().Be(i + 1);
                flashCard.items.Keys.Should().NotContainNulls();
                flashCard.items.Keys.Should().AllSatisfy(ques => ques.Should().NotBeNullOrWhiteSpace().And.NotBeNullOrEmpty());
                flashCard.items.Values.Should().AllSatisfy(ans => ans.Should().NotBeNullOrWhiteSpace().And.NotBeNullOrEmpty());
            }
        }

        [Fact]
        public void DeleteFlashCardTest()
        {
            // Scenario: Delete a single flash card
            // Step 1: Arrange - Generate a flash card and persist it
            string topic = "history";
            int numberOfFlashCards = 3;
            FlashCard flashCard = service.generateFlashCard(topic, numberOfFlashCards);
            service.persistFlashCard(flashCard);
            FlashCard addedFlashCard = service.getAllFlashCards().First();

            // Step 2: Act - Delete the flash card
            bool? result = service.deleteFlashCard(addedFlashCard.Id);

            // Step 3: Assert - Verify the flash card is deleted
            service.getAllFlashCards().Should().BeEmpty();
            result.Should().BeTrue();
        }

        [Fact]
        public void BatchDeleteFlashCardTest()
        {
            // Scenario: Batch delete multiple flash cards
            // Step 1: Arrange - Generate multiple flash cards and persist them
            string topic = "history";
            int numberOfFlashCards = 3;
            for (int i = 0; i < 2; i++)
            {
                FlashCard flashCard = service.generateFlashCard(topic, numberOfFlashCards);
                service.persistFlashCard(flashCard);
            }
            List<FlashCard> addedFlashCards = service.getAllFlashCards();
            List<string> ids = addedFlashCards.Select(flashcard => flashcard.Id).ToList();

            // Step 2: Act - Batch delete the flash cards
            bool result = service.batchDeleteFlashCards(ids).Value;

            // Step 3: Assert - Verify all flash cards are deleted
            service.getAllFlashCards().Should().BeEmpty();
        }

        [Fact]
        public void UpdateFlashCardTest()
        {
            // Scenario: Update a flash card
            // Step 1: Arrange - Generate a flash card and persist it
            string topic = "history";
            int numberOfFlashCards = 3;
            FlashCard flashCard = service.generateFlashCard(topic, numberOfFlashCards);
            service.persistFlashCard(flashCard);
            FlashCard addedFlashCard = service.getAllFlashCards().First();

            // Step 2: Act - Update the flash card
            addedFlashCard.userId = "this is a test";
            service.updateFlashCard(flashCardToUpdate: addedFlashCard);

            // Step 3: Assert - Verify the flash card is updated correctly
            FlashCard updatedFlashCard = service.getAllFlashCards().First();
            updatedFlashCard.userId.Should().Be(addedFlashCard.userId);
        }

        public void Dispose()
        {
            fakeClientWithInMemoryDataLayer.Dispose();
        }
    }
}
