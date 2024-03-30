using DotnetGeminiSDK.Client.Interfaces;
using DotnetGeminiSDK.Model;
using DotnetGeminiSDK.Model.Response;
using Microsoft.AspNetCore.Mvc;

namespace StudyfiedBackend.Controllers.Gemini
{
    public static class GenericGeminiClient
    {

        public static async Task<GeminiMessageResponse> GetTextPrompt(IGeminiClient geminiClient, string textPrompt)
        {
            var response = await geminiClient.TextPrompt(textPrompt);

            if (response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }

        public static async Task<GeminiMessageResponse> getImagePrompt(IGeminiClient geminiClient, string textPrompt, string image)
        {
            var response = await geminiClient.ImagePrompt(textPrompt, image, ImageMimeType.Jpeg);

            if (response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }
    }
}
