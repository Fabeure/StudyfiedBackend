using DotnetGeminiSDK.Client.Interfaces;
using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Controllers.Authentication;
using StudyfiedBackend.Controllers.Gemini;
using StudyfiedBackend.Models;

namespace StudyfiedBackend.Controllers.ChatBot
{
    public class ChatBotService : IChatBotService
    {
        private readonly IGeminiClient _geminiClient;
        private readonly IAuthenticationService _authenticationService;

        public ChatBotService(IGeminiClient geminiClient, IAuthenticationService authenticationService)
        {
            _geminiClient = geminiClient;
            _authenticationService = authenticationService;
        }

        public BaseResponse<string> getChatResponse(string conversation, string token)
        {
            try
            {
                ApplicationUser caller = _authenticationService.AuthenticateTokenAndGetUser(token);
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>(ResultCodeEnum.Failed, null, "USER NOT AUTHORIZED");
            }
            conversation = PromptHelper.addHelperToPrompt(conversation, 4, 0);

            var geminiResponse = GenericGeminiClient.GetTextPrompt(_geminiClient, conversation).Result;

            string responseContents = geminiResponse.Candidates[0].Content.Parts[0].Text;

            return new BaseResponse<string>(ResultCodeEnum.Success, responseContents, "Successfully fetched response");
        }
    }
}
