using DotnetGeminiSDK.Client.Interfaces;
using DotnetGeminiSDK.Model;
using DotnetGeminiSDK.Model.Response;

namespace StudyfiedBackend.Controllers.Gemini
{
    public static class GenericGeminiClient
    {

        public static async Task<GeminiMessageResponse> GetTextPrompt(IGeminiClient geminiClient, string textPrompt)
        {
            var response = await geminiClient.TextPrompt(textPrompt);

            if (response != null && isValidResponseContents(response))
            {
                return response;
            }
            else
            {
                return GetTextPrompt(geminiClient, textPrompt).Result;
            }
        }

        public static async Task<GeminiMessageResponse> getImagePrompt(IGeminiClient geminiClient, string textPrompt, string base64image)
        {
            var response = await geminiClient.ImagePrompt(textPrompt, base64image, ImageMimeType.Jpeg);

            if (response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }

        public static bool isValidResponseContents(GeminiMessageResponse response)
        {
            return response != null &&
                response.Candidates[0] != null &&
                response.Candidates[0].Content != null &&
                response.Candidates[0].Content.Parts[0] != null &&
                response.Candidates[0].Content.Parts[0].Text != null;
        }
    }
}
