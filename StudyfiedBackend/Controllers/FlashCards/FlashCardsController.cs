using Microsoft.AspNetCore.Mvc;
using StudyfiedBackend.BaseResponse;

namespace StudyfiedBackend.Controllers.FlashCards
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlashCardsController : ControllerBase
    {
        private readonly IFlashCardsService _flashCardsService;

        public FlashCardsController(IFlashCardsService flashCardsService)
        {
            _flashCardsService = flashCardsService;
        }

        [HttpPost("getFlashCard")]
        public BaseResponse<Models.FlashCard> getFlashCards(string topic)
        {
            return _flashCardsService.getFlashCardResponse(topic).Result;
        }

    }
}
