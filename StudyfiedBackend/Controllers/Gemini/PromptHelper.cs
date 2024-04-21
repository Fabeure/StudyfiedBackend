using Amazon.Auth.AccessControlPolicy;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace StudyfiedBackend.Controllers.Gemini
{
    public static class PromptHelper
    {

        private static string[] helpers =
        {
            "i want you to generate me 5 question answer pairs. Make sure its exactly 5 pairs. You response should be a plain string, and only follow the formatting rules i will give you. Here is the topic : ",
            @" each question and detailled answer should be seperated by a ':', and each pair of question+answer should be seperated by a ';'. Please
            do not include any return to lines, and give me the question, followed by a ':', followed by the answer, followed by a ';' and then the next question answer pair so on and so on
            of course replace question and aswer with the actual question and actual answer
            DO NOT FORMAT THE RESPONSE IN ANY OTHER WAY, DO NOT WRITE THE WORD QUESTION OR ANSWER FOR ME"
            ,"Review the provided image from educational resources. If it corresponds with a relevant course subject, concisely detail the principal ideas and concepts using suitable academic terms.If the image is unrelated to the course material, clearly denote its irrelevance.","From the provided paragraph, extract and summarize the essential points and concepts. Format your summary using HTML tags and style it with CSS. Exclude any header elements and include only the necessary HTML tags and styles. Place the topic of the content within an <h2> tag."
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
