using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Models;

namespace StudyfiedBackend.Controllers.FlashCards
{
    public interface IFlashCardsService
    {
        public BaseResponse<FlashCard> getFlashCard(string topic);
        public PrimitiveBaseResponse<bool> persistFlashCard(FlashCard flashCardWithUserId);
    }
}
