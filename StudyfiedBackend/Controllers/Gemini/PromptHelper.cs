namespace StudyfiedBackend.Controllers.Gemini
{
    public static class PromptHelper
    {

        private static string[] helpers =
        {
            "i want you to generate me some question answer pairs. Make sure its exactly the amount of pairs i will specify in the topic. You response should be a plain string, and only follow the formatting rules i will give you. Here is the topic : ",
            @" each question and detailled answer should be seperated by a ':', and each pair of question+answer should be seperated by a ';'. Please
            do not include any return to lines, and give me the question, followed by a ':', followed by the answer, followed by a ';' and then the next question answer pair so on and so on
            of course replace question and aswer with the actual question and actual answer
            DO NOT FORMAT THE RESPONSE IN ANY OTHER WAY, DO NOT WRITE THE WORD QUESTION OR ANSWER FOR ME. The first part should of the flashcard is a question, the second is an answer to that question.",
            "**Context: This image is a page from a study material. Task: If the image appears relevant to a course topic, summarize the key points and concepts.Use the language related to the content . Otherwise, indicate that the content is not relevant.",
            "based on this paragraph , summarize the key points and concepts. i want it formatted using html tags and style using css.include only the html tags for the content along with required style required without the header part and make sure to add onmy the topic of its content in <h2> tag..here is ther paragraph:",
            "you are CHATBOT FOR AN EDUCATIONAL WEBSITE, please give me the next reply, do not write the words user or bot, just THE ANSWER GOD DAMNIT, here is our conversation history: "
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
