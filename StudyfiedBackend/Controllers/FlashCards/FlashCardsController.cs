using Microsoft.AspNetCore.Mvc;
using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Models;

namespace StudyfiedBackend.Controllers.FlashCards
{   
    [Route("api/[controller]")]
    [ApiController]
    public class FlashCardsController : ControllerBase
    {
        private readonly IFlashCardsService flashCardsService;

        public FlashCardsController(IFlashCardsService flashCardsService)
        {
            this.flashCardsService = flashCardsService;
        }

        [HttpPost("getFlashCard")]
        public BaseResponse<FlashCard> get(string topic, int numberOfFlashCards)
        {
            return flashCardsService.getFlashCard(topic, numberOfFlashCards);
        }

        [HttpPost("persistFlashCard")]
        public PrimitiveBaseResponse<bool> persistFlashCard(FlashCard flashCardWithUserId)
        {
            return flashCardsService.persistFlashCard(flashCardWithUserId);
        }

        [HttpGet("getExistingFlashCard")]
        public BaseResponse<FlashCard> getExistingFlashCard(string id)
        {
            return flashCardsService.getExistingFlashCard(id);
        }

        [HttpGet("getAllFlashCards")]
        public BaseResponse<List<FlashCard>> getAllFlashCards()
        {
            return flashCardsService.getAllFlashCards();
        }

    }
}
