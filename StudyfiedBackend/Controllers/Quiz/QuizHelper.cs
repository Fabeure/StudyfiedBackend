using DotnetGeminiSDK.Client.Interfaces;
using DotnetGeminiSDK.Model.Response;
using StudyfiedBackend.Controllers.Gemini;
using StudyfiedBackend.Models;

namespace StudyfiedBackend.Controllers.Quize

{
    public class QuizHelper
    {
        public static List<Question> GenerateQuestion(string topic, string difficulty,int numberOfQuestions, IGeminiClient geminiClient)
        {
            List<Question> questions = new List<Question>();
            var questionByDifficulty = numberOfQuestions + " " + difficulty;

            string prompt = "i want you to generate me "; +questionByDifficulty+" multichoice questions. Make sure its exactly "+numberOfQuestions+" questions. You response should be a plain string, " +
                "and only follow the formatting rules i will give you. Here is the topic : " +
                topic + " the format is that questions should be seperated by a ';' between every single one of them. " +
                "Please do not include any return to lines, and give me the question followed by \";\" ,and only \";\" without any return to lines DO NOT INCLUDE ANY RETURN TO LINES '\n'" +
                "DO NOT FORMAT THE RESPONSE IN ANY OTHER WAY, DO NOT WRITE THE WORD QUESTION FOR ME";

            var geminiResponse = GenericGeminiClient.GetTextPrompt(geminiClient, prompt).Result;
            if (geminiResponse != null)
            {
                string[] questionsArray = geminiResponse.Candidates[0].Content.Parts[0].Text.Split(';');
                foreach (string question in questionsArray)
                {
                    questions.Add(new Question(question));
                }
            }
            return questions;
        }
        public static List<Response> GenerateResponses(Question question, IGeminiClient geminiClient)
        {
            List<Response> responses = new List<Response>();
            string prompt = "i want you to generate me 4 answer option and validity(true/false) pairs. Make sure its exactly 4 pairs. You response should be a " +
                "plain string, and only follow the formatting rules i will give you. Here is the question :" + question.question +
                "each option and validity should be seperated by only and only a ':', and each pair of option+validity should be seperated by a" +
                " ';'. Please do not include any return to lines, and give me the option, followed by only a ':', followed by the its validity, " +
                "followed by a ';' and then the next option validity pair so on and so on of course replace option and validity with the actual " +
                "option and actual validity DO NOT FORMAT THE RESPONSE IN ANY OTHER WAY, DO NOT WRITE THE WORD OPTION OR VALIDITY FOR ME";

            var geminiResponse = GenericGeminiClient.GetTextPrompt(geminiClient, prompt).Result;

            if (geminiResponse != null)
            {
                string[] responsesArray = geminiResponse.Candidates[0].Content.Parts[0].Text.Split(';');
                foreach (string response in responsesArray)
                {
                    string[] parts = response.Split(":");
                    if (parts.Length == 2)
                    {
                        string validity = parts[1].ToLower().Trim();
                        if (validity == "true")
                        {
                            responses.Add(new Response(parts[0], true));
                        }
                        else if (validity == "false")
                        {
                            responses.Add(new Response(parts[0], false));
                        }   
                    }
                }
            }
            return responses;
        }
    }

}
