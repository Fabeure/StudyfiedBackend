namespace StudyfiedBackend.Controllers.Gemini
{
    public static class PromptHelper
    {

        private static string[] helpers =
        {
            " Please make sure that your reply will be in the form of comma seperated strings, it is imperative that the elements of your response are seperated by a comma,do not include return to lines, do not include any special chars other than commas.",
        };

        public static string addHelperToPrompt(string prompt, int HelperCode, int position)
        {
            switch (position)
            {
                case 0:
                    return helpers[HelperCode] + prompt;
                    break;
                case 1:
                    return prompt + helpers[HelperCode];
                    break;
                default:
                    return prompt;
            }
        }
    }
}
