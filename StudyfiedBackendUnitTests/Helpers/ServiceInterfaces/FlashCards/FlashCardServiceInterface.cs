using DotnetGeminiSDK.Model.Request;
using Newtonsoft.Json;
using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace StudyfiedBackendUnitTests.Helpers.ServiceInterfaces.FlashCards
{
    internal class FlashCardServiceInterface : BaseServiceInterface
    {
        public FlashCardServiceInterface(HttpClient client) : base(client) {}

        public BaseResponse<List<FlashCard>> getBatchExistingFlashCard(HttpClient client, string[] id)
        {
            throw new NotImplementedException();
        }

        public List<FlashCard> getAllFlashCards()
        {
            var url = $"{URLEnums.FLASHCARDS}/getAllFlashCards";
            var response = _httpClient.GetAsync(url).Result;

            BaseResponse<List<FlashCard>>? deserializedResponse = response.Content.ReadFromJsonAsync<BaseResponse<List<FlashCard>>>().Result;

            if (deserializedResponse == null || deserializedResponse.ResultItem == null) { throw new Exception(message: "Error deserializing response. Please check your return type"); }
            return deserializedResponse.ResultItem;
        }

        public bool? updateFlashCard(FlashCard flashCardToUpdate)
        {
            var url = $"{URLEnums.FLASHCARDS}/updateFlashCard";

            // Prepare JSON string from FlashCard object
            var jsonContent = JsonConvert.SerializeObject(flashCardToUpdate);
            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = _httpClient.PostAsync(url, stringContent).Result;
            PrimitiveBaseResponse<bool>? deserializedResponse = response.Content.ReadFromJsonAsync<PrimitiveBaseResponse<bool>>().Result;

            if (deserializedResponse == null) { throw new Exception(message: "Error deserializing response. Please check your return type"); }
            return deserializedResponse.ResultItem;
        }

        public FlashCard? getFlashCardById(string id)
        {
            var url = $"{URLEnums.FLASHCARDS}/getFlashCardById?{nameof(id)}={id}";

            var response = _httpClient.GetAsync(url).Result;

            BaseResponse<FlashCard>? deserializedResponse = response.Content.ReadFromJsonAsync<BaseResponse<FlashCard>>().Result;

            if (deserializedResponse == null || deserializedResponse.ResultItem == null) { throw new Exception(message: "Error deserializing response. Please check your return type"); }
            return deserializedResponse.ResultItem;
        }

        public FlashCard generateFlashCard(string topic, int numberOfFlashCards)
        {
            var url = $"{URLEnums.FLASHCARDS}/generateFlashCard?{nameof(topic)}={topic}&{nameof(numberOfFlashCards)}={numberOfFlashCards}";

            var response = _httpClient.PostAsync(url, null).Result;

            BaseResponse<FlashCard>? deserializedResponse = response.Content.ReadFromJsonAsync<BaseResponse<FlashCard>>().Result;

            if (deserializedResponse == null || deserializedResponse.ResultItem == null) { throw new Exception(message: "Error deserializing response. Please check your return type"); }
            return deserializedResponse.ResultItem;
        }

        public bool? persistFlashCard(FlashCard flashCardWithUserId)
        {
            var url = $"{URLEnums.FLASHCARDS}/persistFlashCard";

            // Prepare JSON string from FlashCard object
            var jsonContent = JsonConvert.SerializeObject(flashCardWithUserId);
            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = _httpClient.PostAsync(url, stringContent).Result;
            PrimitiveBaseResponse<bool>? deserializedResponse = response.Content.ReadFromJsonAsync<PrimitiveBaseResponse<bool>>().Result;

            if (deserializedResponse == null) { throw new Exception(message: "Error deserializing response. Please check your return type"); }
            return deserializedResponse.ResultItem;
        }

        public bool? deleteFlashCard(string id)
        {
            var url = $"{URLEnums.FLASHCARDS}/deleteFlashCard?{nameof(id)}={id}";
            var response = _httpClient.PostAsync(url, null).Result;

            PrimitiveBaseResponse<bool>? deserializedResponse = response.Content.ReadFromJsonAsync<PrimitiveBaseResponse<bool>>().Result;

            if (deserializedResponse == null) { throw new Exception(message: "Error deserializing response. Please check your return type"); }
            return deserializedResponse.ResultItem;
        }

        public bool? batchDeleteFlashCards(IEnumerable<string> ids)
        {
            var url = $"{URLEnums.FLASHCARDS}/batchDeleteFlashCards";

            // Prepare JSON string from FlashCard object
            var jsonContent = JsonConvert.SerializeObject(ids);
            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");


            var response = _httpClient.PostAsync(url, stringContent).Result;

            PrimitiveBaseResponse<bool>? deserializedResponse = response.Content.ReadFromJsonAsync<PrimitiveBaseResponse<bool>>().Result;

            if (deserializedResponse == null) { throw new Exception(message: "Error deserializing response. Please check your return type"); }
            return deserializedResponse.ResultItem;
        }
    }
}
