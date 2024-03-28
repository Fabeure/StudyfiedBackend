using StudyfiedBackend.BaseResponse;

namespace StudyfiedBackend.Controllers.FlashCards
{
    public interface IFlashCardsService
    {
        public Task<BaseResponse<Models.FlashCard>> getFlashCardResponse(string topic);
    }
}
