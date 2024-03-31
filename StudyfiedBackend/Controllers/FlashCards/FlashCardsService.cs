using DotnetGeminiSDK.Client.Interfaces;
using StudyfiedBackend.Models;
using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Controllers.Gemini;
using StudyfiedBackend.DataLayer;

namespace StudyfiedBackend.Controllers.FlashCards
{
    public class FlashCardsService : IFlashCardsService
    {
        private readonly IGeminiClient _geminiClient;
        private readonly IMongoRepository<FlashCard> _flashCardRepository;

        public FlashCardsService(IGeminiClient geminiClient, IMongoContext context) {
            _geminiClient = geminiClient;
            _flashCardRepository = context.GetRepository<FlashCard>();
        }
        public BaseResponse<FlashCard> getFlashCard(string topic)
        {
            if (topic == null || topic == "")
            {
                return new BaseResponse<FlashCard>(ResultCodeEnum.Failed, null);
            }

            topic = PromptHelper.addHelperToPrompt(topic, 0, 0);

            var geminiResponse = GenericGeminiClient.GetTextPrompt(_geminiClient, topic).Result;

            if (geminiResponse != null)
            {
                return new BaseResponse<FlashCard>(ResultCodeEnum.Success, FlashCardsHelpers.processFlashCardResponse(geminiResponse), "Succesfully fetched FlashCards");
            }
            else
            {
                return new BaseResponse<FlashCard>(ResultCodeEnum.Failed, null);
            }
        }
        public PrimitiveBaseResponse<bool> persistFlashCard(FlashCard flashCardWithUserId)
        {
            FlashCard addedFlashCard = _flashCardRepository.CreateAsync(flashCardWithUserId).Result;
            if (addedFlashCard != null)
            {
                return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Success, true, $"FlashCard added successfully for user {flashCardWithUserId.userId}");
            }
            return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Failed, false, $"FlashCard not added for user {flashCardWithUserId.userId}");
        }
    }
}
