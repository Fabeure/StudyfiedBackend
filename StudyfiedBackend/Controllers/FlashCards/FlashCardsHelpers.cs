using DotnetGeminiSDK.Model.Response;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace StudyfiedBackend.Controllers.FlashCards
{
    public static class FlashCardsHelpers
    {
        public static string filterRegEx = "";
        public static Models.FlashCard processFlashCardResponse(GeminiMessageResponse response)
        {
            string responseText = response.Candidates[0].Content.Parts[0].Text;
            string[] flashCardStrings = doProcessFlashCardReponse(data: responseText);
            Models.FlashCard flashCard = buildFlashCardResponse(flashCardStrings);

            return flashCard;
        }

        public static string[] doProcessFlashCardReponse(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return Array.Empty<string>();
            }
            data = Regex.Replace(data.Trim(), filterRegEx, "");
            return data.Split(',');
        }

        public static Models.FlashCard buildFlashCardResponse(string[] items)
        {
            Models.FlashCard flashCard = new Models.FlashCard(items);
            return flashCard;
        }
    }
}
