using DotnetGeminiSDK.Client.Interfaces;
using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Controllers.Gemini;
using StudyfiedBackend.Models;

namespace StudyfiedBackend.Controllers.ChatBot
{
    public class ChatBotService : IChatBotService
    {
        private readonly IGeminiClient _geminiClient;

        public ChatBotService(IGeminiClient geminiClient)
        {
            _geminiClient = geminiClient;
        }

        public BaseResponse<string> getChatResponse(string conversation)
        {
            conversation = PromptHelper.addHelperToPrompt(conversation, 4, 0);

            var geminiResponse = GenericGeminiClient.GetTextPrompt(_geminiClient, conversation).Result;

            string responseContents = geminiResponse.Candidates[0].Content.Parts[0].Text;

            return new BaseResponse<string>(ResultCodeEnum.Success, responseContents, "Successfully fetched response");
        }
    }
}
