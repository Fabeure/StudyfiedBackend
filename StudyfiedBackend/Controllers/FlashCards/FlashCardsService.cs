using DotnetGeminiSDK.Client.Interfaces;
using Microsoft.AspNetCore.Mvc;
using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Controllers.Gemini;

namespace StudyfiedBackend.Controllers.FlashCards
{
    public class FlashCardsService : IFlashCardsService
    {
        private readonly IGeminiClient _geminiClient;

        public FlashCardsService(IGeminiClient geminiClient) {
            _geminiClient = geminiClient;
        }
        public async Task<BaseResponse<Models.FlashCard>> getFlashCardResponse(string topic)
        {
            if (topic == null || topic == "")
            {
                return new BaseResponse<Models.FlashCard>(ResultCodeEnum.Failed, null);
            }

            topic = PromptHelper.addHelperToPrompt(topic, 0, 0);

            var geminiResponse = await GenericGeminiClient.GetTextPrompt(_geminiClient, topic);

            if (geminiResponse != null)
            {
                return new BaseResponse<Models.FlashCard>(ResultCodeEnum.Success, FlashCardsHelpers.processFlashCardResponse(geminiResponse));
            }
            else
            {
                return new BaseResponse<Models.FlashCard>(ResultCodeEnum.Failed, null);
            }
        }
    }
}
