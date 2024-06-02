using StudyfiedBackend.BaseResponse;
namespace StudyfiedBackend.Controllers.ChatBot
{
    public interface IChatBotService
    {
        public BaseResponse<string> getChatResponse(string conversation);
    }
}
