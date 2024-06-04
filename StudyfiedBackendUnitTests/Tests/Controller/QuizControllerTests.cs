using FluentAssertions;
using StudyfiedBackend.Models;
using StudyfiedBackendUnitTests.Helpers.ServiceInterfaces.FlashCards;
using StudyfiedBackendUnitTests.Mock.DataLayer;
using System.Diagnostics;

namespace StudyfiedBackendUnitTests.Tests.Controller
{
    [Collection("Database collection")]
    public class QuizControllerTests : IDisposable
    {
        private readonly FakeServiceWithInMemoryDataLayer fakeClientWithInMemoryDataLayer;
        private readonly QuizServiceInterface service;
        public QuizControllerTests(FakeServiceWithInMemoryDataLayer fakeClientWithInMemoryDataLayer)
        {
            this.fakeClientWithInMemoryDataLayer = fakeClientWithInMemoryDataLayer;
            service = new QuizServiceInterface(client: this.fakeClientWithInMemoryDataLayer.FakeHttpClient);
        }

        [Fact]
        public void getQuizTest()
        {
            // Scenario: Generate Quiz
            // Step 1: Arrange - Define topic, difficulty for quiz
            string randomTopic = "history";
            string difficulty = "medium";
            int numberOfQuestion = 4;

            // Step 2: Act - Generate flash cards
            Quiz quiz = service.generateQuiz(randomTopic, difficulty, numberOfQuestion);

            // Step 3: Assert - Verify each generated flash card
            quiz.Should().NotBeNull();
        }
        public void Dispose()
        {
            fakeClientWithInMemoryDataLayer.Dispose();
        }
    }
}
