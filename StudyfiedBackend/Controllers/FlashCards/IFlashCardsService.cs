﻿using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Models;

namespace StudyfiedBackend.Controllers.FlashCards
{
    public interface IFlashCardsService
    {
        public BaseResponse<FlashCard> getFlashCard(string topic, int numberOfFlashCards);
        public PrimitiveBaseResponse<bool> persistFlashCard(FlashCard flashCardWithUserId);
        public BaseResponse<FlashCard> getExistingFlashCard(string id);
        public BaseResponse<List<FlashCard>> getBatchExistingFlashCard(string[] ids);
        public BaseResponse<List<FlashCard>> getFlashCardsByUserId(string userId);
        public BaseResponse<List<FlashCard>> getAllFlashCards();
        public PrimitiveBaseResponse<bool> deleteFlashCard(string id);
        public PrimitiveBaseResponse<bool> updateFlashCard(FlashCard updatedFlashCard);
        public PrimitiveBaseResponse<bool> deleteBatchFlashCard(string[] id);

        public PrimitiveBaseResponse<bool> batchDeleteFlashCards(string[] ids);
    }
}
