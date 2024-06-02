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
            ,"Please carefully examine the image provided and evaluate its relevance to any specific course subject. If you believe the image is related to course material, please summarize the main ideas and concepts depicted using academic terminology. If the image is not connected to any course content, please clearly state its lack of relevance. Your response should be structured to improve clarity and readability and make sure to keep the language used in the image.","From the provided paragraph, extract and explain the essential points  and concepts using examples if possible. Format your explain using HTML tags and style it with CSS. Exclude any header elements and include only the necessary HTML tags and styles. Place the topic of the content within an <h2> tag. And make sure to use  the same language of the paragraphs."
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
