﻿using Microsoft.AspNetCore.Mvc;
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
        public BaseResponse<FlashCard> get([FromBody] string topic)
        {
            return flashCardsService.getFlashCardResponse(topic).Result;
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
