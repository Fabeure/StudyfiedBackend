using Microsoft.AspNetCore.Mvc;
using StudyfiedBackend.BaseResponse;

namespace StudyfiedBackend.Controllers.ChatBot
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatBotController
    {
        private readonly IChatBotService _chatBotService;

        public ChatBotController(IChatBotService chatBotService)
        {
            _chatBotService = chatBotService;
        }

        [HttpGet("generateResponse")]
        public BaseResponse<string> generateResponse(string conversation, string token)
        {
            return _chatBotService.getChatResponse(conversation, token);
        }
    }
}
