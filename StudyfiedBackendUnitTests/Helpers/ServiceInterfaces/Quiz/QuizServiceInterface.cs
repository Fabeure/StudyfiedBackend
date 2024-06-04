using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Models;
using System.Net.Http.Json;

namespace StudyfiedBackendUnitTests.Helpers.ServiceInterfaces.FlashCards
{
    internal class QuizServiceInterface : BaseServiceInterface
    {
        public QuizServiceInterface(HttpClient client) : base(client) { }


        public Quiz generateQuiz(string topic, string difficulty, int numberOfQuestion)
        {
            string token = "testToken";
            var url = $"{URLEnums.QUIZ}/getQuiz?{nameof(topic)}={topic}&{nameof(difficulty)}={difficulty}&{nameof(numberOfQuestion)}={numberOfQuestion}&{nameof(token)}={token}";

            var response = _httpClient.PostAsync(url, null).Result;

            BaseResponse<Quiz>? deserializedResponse = response.Content.ReadFromJsonAsync<BaseResponse<Quiz>>().Result;

            if (deserializedResponse == null || deserializedResponse.ResultItem == null) { throw new Exception(message: "Error deserializing response. Please check your return type"); }
            return deserializedResponse.ResultItem;
        }
    }
}
