using DotnetGeminiSDK.Model.Response;
using StudyfiedBackend.Models;
using System.Text.RegularExpressions;

namespace StudyfiedBackend.Controllers.FlashCards
{
    public static class FlashCardsHelpers
    {
        public static string filterRegEx = "";
        public static FlashCard processFlashCardResponse(GeminiMessageResponse response)
        {
            string responseText = response.Candidates[0].Content.Parts[0].Text;
            string[] flashCardContents = doProcessFlashCardObject(data: responseText);
            FlashCard flashCard = buildFlashCardObject(flashCardContents);

            return flashCard;
        }

        public static string[] doProcessFlashCardObject(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return Array.Empty<string>();
            }
            data = Regex.Replace(data.Trim(), filterRegEx, "");
            return data.Split(',');
        }

        public static FlashCard buildFlashCardObject(string[] items)
        {
            FlashCard flashCard = new FlashCard(items);
            return flashCard;
        }
    }
}
