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

        [HttpPost("generateFlashCard")]
        public BaseResponse<FlashCard> generateFlashCard(string topic, int numberOfFlashCards, string token)
        {
            return flashCardsService.generateFlashCard(topic, numberOfFlashCards, token);
        }

        [HttpPost("persistFlashCard")]
        public PrimitiveBaseResponse<bool> persistFlashCard(FlashCard flashCardWithUserId, string token)
        {
            return flashCardsService.persistFlashCard(flashCardWithUserId, token);
        }

        [HttpGet("getFlashCardById")]
        public BaseResponse<FlashCard> getFlashCardById(string id)
        {
            return flashCardsService.getFlashCardById(id);
        }

        [HttpGet("getFlashCardsByUserId")]
        public BaseResponse<List<FlashCard>> getFlashCardsByUserId(string userId, string token)
        {
            return flashCardsService.getFlashCardsByUserId(userId, token);
        }

        [HttpGet("getAllFlashCards")]
        public BaseResponse<List<FlashCard>> getAllFlashCards()
        {
            return flashCardsService.getAllFlashCards();
        }

        [HttpPost("updateFlashCard")]
        public PrimitiveBaseResponse<bool> updateFlashCard(FlashCard flashCard)
        {
            return flashCardsService.updateFlashCard(flashCard);
        }

        [HttpPost("deleteFlashCard")]
        public PrimitiveBaseResponse<bool> deleteFlashCard(string id)
        {
            return flashCardsService.deleteFlashCard(id);
        }

        [HttpPost("batchDeleteFlashCards")]
        public PrimitiveBaseResponse<bool> batchDeleteFlashCards(string[] ids)
        {
            return flashCardsService.batchDeleteFlashCards(ids);
        }

    }
}
