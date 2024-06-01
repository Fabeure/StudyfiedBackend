using DotnetGeminiSDK.Client.Interfaces;
using StudyfiedBackend.Models;
using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Controllers.Gemini;
using StudyfiedBackend.DataLayer;
using StudyfiedBackend.DataLayer.Repositories.GenericMongoRepository;

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
        public BaseResponse<FlashCard> getFlashCard(string topic, int numberOfFlashcards)
        {
            if (topic == null || topic == "")
            {
                return new BaseResponse<FlashCard>(ResultCodeEnum.Failed, null);
            }

            topic = FlashCardsHelpers.insertNumberOfFlashCardsToTopic(topic, numberOfFlashcards);
            topic = PromptHelper.addHelperToPrompt(topic, 0, 0);
            topic = PromptHelper.addHelperToPrompt(topic, 1, 1);

            var geminiResponse = GenericGeminiClient.GetTextPrompt(_geminiClient, topic).Result;

            if (geminiResponse != null)
            {
                FlashCard flashCard = FlashCardsHelpers.processFlashCardResponse(geminiResponse);
                if (!FlashCardsHelpers.validateFlashCardResult(flashCard, numberOfFlashcards)) {
                    return getFlashCard(topic, numberOfFlashcards);
                }
                return new BaseResponse<FlashCard>(ResultCodeEnum.Success, flashCard, "Succesfully fetched FlashCards");
            }
            else
            {
                return new BaseResponse<FlashCard>(ResultCodeEnum.Failed, null);
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

        public BaseResponse<FlashCard> getExistingFlashCard(string id)
        {
            FlashCard flashCard = _flashCardRepository.GetByIdAsync(id).Result;
            if (flashCard != null)
            {
                return new BaseResponse<FlashCard>(ResultCodeEnum.Success, flashCard, $"Succesfully fetched flash card {flashCard.Id}");
            }
            return new BaseResponse<FlashCard>(ResultCodeEnum.Failed, null, $"No flash card with id {id} found");
        }

        public BaseResponse<List<FlashCard>> getBatchExistingFlashCard(string[] id)
        {
            List<FlashCard> flashCards = _flashCardRepository.GetDocumentsByIdsAsync(id).Result.ToList();
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

        public PrimitiveBaseResponse<bool> deleteBatchFlashCard(string[] id)
        {
           bool deleted = _flashCardRepository.BatchDelete(id).Result;
            if (deleted)
            {
                return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Success, true, $"FlashCards with ids : {id} deleted successfully ");
            }
            return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Failed, false, $"FlashCards not deleted");    
        }   
        public BaseResponse<List<FlashCard>> getAllFlashCards()
        {
            List<FlashCard> flashCards = _flashCardRepository.GetAllAsync().Result.ToList();
            return new BaseResponse<List<FlashCard>>(ResultCodeEnum.Success, flashCards, "flashcards fetched!");
        }
    }
}
