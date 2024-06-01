using DotnetGeminiSDK.Client.Interfaces;
using StudyfiedBackend.Models;
using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Controllers.Gemini;
using StudyfiedBackend.DataLayer;
using StudyfiedBackend.DataLayer.Repositories.GenericMongoRepository;
using MongoDB.Driver;

namespace StudyfiedBackend.Controllers.FlashCards
{
    public class FlashCardsService : IFlashCardsService
    {
        private readonly IGeminiClient _geminiClient;
        private readonly IMongoRepository<FlashCard> _flashCardRepository;

        public FlashCardsService(IGeminiClient geminiClient, IMongoContext context)
        {
            _geminiClient = geminiClient;
            _flashCardRepository = context.GetRepository<FlashCard>();
        }
        public BaseResponse<FlashCard> generateFlashCard(string topic, int numberOfFlashcards)
        {
            if (topic == null || topic == "")
            {
                return new BaseResponse<FlashCard>(ResultCodeEnum.Failed, null);
            }

            topic = FlashCardsHelpers.insertNumberOfFlashCardsToTopic(topic, numberOfFlashcards);
            topic = PromptHelper.addHelperToPrompt(topic, 0, 0);
            topic = PromptHelper.addHelperToPrompt(topic, 1, 1);

            bool isValidFlashCard = false;
            FlashCard flashCard = new FlashCard(new Dictionary<string, string>());
            try
            {
                while (!isValidFlashCard)
                {
                    var geminiResponse = GenericGeminiClient.GetTextPrompt(_geminiClient, topic).Result;
                    flashCard = FlashCardsHelpers.processFlashCardResponse(geminiResponse);
                    isValidFlashCard = FlashCardsHelpers.validateFlashCardResult(flashCard, numberOfFlashcards);
                }
                return new BaseResponse<FlashCard>(ResultCodeEnum.Success, flashCard, "Succesfully fetched FlashCards");
            }
            catch (Exception e)
            {
                return new BaseResponse<FlashCard>(ResultCodeEnum.Failed, null, "Error fetching flashcard, please try again");
            }
        }
        public PrimitiveBaseResponse<bool> persistFlashCard(FlashCard flashCardWithUserId)
        {
            FlashCard addedFlashCard = _flashCardRepository.CreateAsync(flashCardWithUserId).Result;
            if (addedFlashCard != null)
            {
                return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Success, true, $"FlashCard added successfully for user {flashCardWithUserId.userId}");
            }
            return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Failed, false, $"FlashCard not added for user {flashCardWithUserId.userId}");
        }

        public BaseResponse<FlashCard> getFlashCardById(string id)
        {
            FlashCard flashCard = _flashCardRepository.GetByIdAsync(id).Result;
            if (flashCard != null)
            {
                return new BaseResponse<FlashCard>(ResultCodeEnum.Success, flashCard, $"Succesfully fetched flash card {flashCard.Id}");
            }
            return new BaseResponse<FlashCard>(ResultCodeEnum.Failed, null, $"No flash card with id {id} found");
        }

        public BaseResponse<List<FlashCard>> getBatchFlashCardsByIds(string[] ids)
        {
            List<FlashCard> flashCards = _flashCardRepository.GetDocumentsByIdsAsync(ids).Result.ToList();
            if (flashCards != null)
            {
                return new BaseResponse<List<FlashCard>>(ResultCodeEnum.Success, flashCards, "flashCards fetched");
            }
            return new BaseResponse<List<FlashCard>>(ResultCodeEnum.Failed, null, "failed to fetch flashCards");
        }
        
        public PrimitiveBaseResponse<bool> updateFlashCard (FlashCard updatedFlashCard)
        {
            if (updatedFlashCard == null || string.IsNullOrEmpty(updatedFlashCard.Id))
            {
                return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Failed, false, "FlashCard not found");
            }
            bool updated = _flashCardRepository.UpdateAsync(updatedFlashCard.Id, updatedFlashCard).Result;
            if (updated)
            {
                return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Success, true, $"FlashCard updated successfully for user {updatedFlashCard.userId}");
            }
            return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Failed, false, $"FlashCard not updated for user {updatedFlashCard.userId}");
        }

        public PrimitiveBaseResponse<bool> deleteFlashCard(string id)
        {
            bool deleted = _flashCardRepository.DeleteAsync(id).Result;
            if (deleted)
            {
                return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Success, true, $"FlashCard with id : {id} deleted successfully ");
            }
            return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Failed, false, $"FlashCard not deleted");
        }

        public PrimitiveBaseResponse<bool> batchDeleteFlashCards(string[] ids)
        {
           bool deleted = _flashCardRepository.BatchDelete(ids).Result;
            if (deleted)
            {
                return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Success, true, $"FlashCards with ids : {ids} deleted successfully ");
            }
            return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Failed, false, $"FlashCards not deleted");    
        }   
        public BaseResponse<List<FlashCard>> getAllFlashCards()
        {
            List<FlashCard> flashCards = _flashCardRepository.GetAllAsync().Result.ToList();
            return new BaseResponse<List<FlashCard>>(ResultCodeEnum.Success, flashCards, "flashcards fetched!");
        }

        public BaseResponse<List<FlashCard>> getFlashCardsByUserId(string userId)
        {
            var filter = Builders<FlashCard>.Filter.Eq("userId", userId);
            List<FlashCard> flashCards = _flashCardRepository.GetByFilter(filter).Result.ToList();

            if (flashCards != null)
            {
                return new BaseResponse<List<FlashCard>>(ResultCodeEnum.Success, flashCards, $"found {flashCards.Count()} flashcards with user id : {userId}");
            }
            return new BaseResponse<List<FlashCard>>(ResultCodeEnum.Failed, null, "failed to fetch flashCards");
        }
    }
}
