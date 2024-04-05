using DotnetGeminiSDK.Model.Response;
using StudyfiedBackend.Models;

namespace StudyfiedBackend.Controllers.FlashCards
{
    public static class FlashCardsHelpers
    {
        public static FlashCard processFlashCardResponse(GeminiMessageResponse response)
        {
            string responseText = response.Candidates[0].Content.Parts[0].Text;
            Dictionary<string, string> flashCardContents = doProcessFlashCardObject(data: responseText);
            FlashCard flashCard = buildFlashCardObject(flashCardContents);

            return flashCard;
        }

        public static Dictionary<string, string> doProcessFlashCardObject(string data)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            // Split the string into individual title-info pairs
            data = data.Trim();
            string[] pairs = data.Split(';');

            foreach (string pair in pairs)
            {
                string[] parts = pair.Split(':');

                if (parts.Length == 2)
                {
                    string question = parts[0].Trim();
                    string answer = parts[1].Trim();

                    result[question] = answer;
                }
                // Handle cases where there might be extra/missing information
                else
                {
                }
            }

            return result;
        }

        public static FlashCard buildFlashCardObject(Dictionary<string, string> items)
        {
            FlashCard flashCard = new FlashCard(items);
            return flashCard;
        }

        public static bool validateFlashCardResult(FlashCard flashCard)
        {
            return (flashCard.items.Count >= 5
                && flashCard.items.All(pair => pair.Value != "" && pair.Key != ""));
        }
    }
}
