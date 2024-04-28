using DotnetGeminiSDK.Model.Response;
using Spire.Pdf;
using StudyfiedBackend.Models;


namespace StudyfiedBackend.Controllers.Resumes
{
    public static class ResumesHelpers
    {
        public static void processResumesResponse(Resume resume, GeminiMessageResponse summarizedPageResponse)
        {
            string summarizedPageText = summarizedPageResponse.Candidates[0].Content.Parts[0].Text;

            buildResumeCardObject(resume, summarizedPageText);
        }

        public static void buildResumeCardObject(Resume resume, string resumeContent)
        {
            resume.ResumeContents.Add(resumeContent);
        }

        public static bool validateResumeObject(Resume resume, int numberOfPages)
        {
            return resume != null
                && numberOfPages > 0
                && resume.ResumeContents.Count == numberOfPages
                && resume.ResumeContents.All(pageResume => pageResume != null && pageResume != "");
        }

        public static PdfDocument getPdfFromString64(string encodedPdf) {

            MemoryStream ms = new MemoryStream(Convert.FromBase64String(encodedPdf));

            PdfDocument doc = new PdfDocument();

            doc.LoadFromStream(ms);

            return doc;
        }
    }
}
