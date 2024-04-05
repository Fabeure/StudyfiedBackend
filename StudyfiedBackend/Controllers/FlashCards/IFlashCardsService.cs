using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Models;

namespace StudyfiedBackend.Controllers.FlashCards
{
    public interface IFlashCardsService
    {
        public Task<BaseResponse<FlashCard>> getFlashCardResponse(string topic);
    }
}
