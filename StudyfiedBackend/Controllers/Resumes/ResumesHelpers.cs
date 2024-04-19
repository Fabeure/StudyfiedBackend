using DotnetGeminiSDK.Model.Response;
using StudyfiedBackend.Models;


namespace StudyfiedBackend.Controllers.Resumes
{
    public static class ResumesHelpers
    {
        public static Resume processResumesResponse(GeminiMessageResponse response)
        {
            string responseText = response.Candidates[0].Content.Parts[0].Text;
            Resume resume = buildResumeCardObject(responseText);
            return resume;
        }

        public static Resume buildResumeCardObject(string resumeContent)
        {
            Resume resume = new Resume(resumeContent,"topic");

            return resume;
        }
    }
}
