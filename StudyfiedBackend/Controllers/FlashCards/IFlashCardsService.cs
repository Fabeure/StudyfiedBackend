using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Models;

namespace StudyfiedBackend.Controllers.FlashCards
{
    public interface IFlashCardsService
    {
        public BaseResponse<FlashCard> generateFlashCard(string topic, int numberOfFlashCards);
        public PrimitiveBaseResponse<bool> persistFlashCard(FlashCard flashCardWithUserId);
        public BaseResponse<FlashCard> getFlashCardById(string id);
        public BaseResponse<List<FlashCard>> getBatchFlashCardsByIds(string[] ids);
        public BaseResponse<List<FlashCard>> getFlashCardsByUserId(string userId);
        public BaseResponse<List<FlashCard>> getAllFlashCards();
        public PrimitiveBaseResponse<bool> deleteFlashCard(string id);
        public PrimitiveBaseResponse<bool> batchDeleteFlashCards(string[] ids);
        public PrimitiveBaseResponse<bool> updateFlashCard(FlashCard updatedFlashCard);
    }
}
