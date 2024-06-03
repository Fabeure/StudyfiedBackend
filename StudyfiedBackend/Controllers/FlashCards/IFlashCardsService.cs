using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Models;

namespace StudyfiedBackend.Controllers.FlashCards
{
    public interface IFlashCardsService
    {
        public BaseResponse<FlashCard> generateFlashCard(string topic, int numberOfFlashCards, string token);
        public PrimitiveBaseResponse<bool> persistFlashCard(FlashCard flashCardWithUserId, string token);
        public BaseResponse<FlashCard> getFlashCardById(string id);
        public BaseResponse<List<FlashCard>> getBatchFlashCardsByIds(string[] ids);
        public BaseResponse<List<FlashCard>> getFlashCardsByUserId(string userId, string token);
        public BaseResponse<List<FlashCard>> getAllFlashCards();
        public PrimitiveBaseResponse<bool> deleteFlashCard(string id);
        public PrimitiveBaseResponse<bool> batchDeleteFlashCards(string[] ids);
        public PrimitiveBaseResponse<bool> updateFlashCard(FlashCard updatedFlashCard);
    }
}
