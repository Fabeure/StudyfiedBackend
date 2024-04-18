namespace StudyfiedBackend.Controllers.Gemini
{
    public static class PromptHelper
    {

        private static string[] helpers =
        {
            "i want you to generate me 5 question answer pairs. Make sure its exactly 5 pairs. You response should be a plain string, and only follow the formatting rules i will give you. Here is the topic : ",
            @" each question and detailled answer should be seperated by a ':', and each pair of question+answer should be seperated by a ';'. Please
            do not include any return to lines, and give me the question, followed by a ':', followed by the answer, followed by a ';' and then the next question answer pair so on and so on
            of course replace question and aswer with the actual question and actual answer. The first part should of the flashcard is a question, the second is an answer to that question.
            DO NOT FORMAT THE RESPONSE IN ANY OTHER WAY, DO NOT WRITE THE WORD QUESTION OR ANSWER FOR ME"
        };

        public static string addHelperToPrompt(string prompt, int HelperCode, int position)
        {
            switch (position)
            {
                case 0:
                    return helpers[HelperCode] + prompt;
                case 1:
                    return prompt + helpers[HelperCode];
                default:
                    return prompt;
            }
        }
    }
}
