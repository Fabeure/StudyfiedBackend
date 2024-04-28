using DotnetGeminiSDK.Client.Interfaces;
using StudyfiedBackend.Models;
using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Controllers.Gemini;
using StudyfiedBackend.DataLayer;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System.Drawing;
using System.Drawing.Imaging;


namespace StudyfiedBackend.Controllers.Resumes
{
    public class ResumesService:IResumesService
    {
        private readonly IGeminiClient _geminiClient;
        private readonly IMongoRepository<Resume> _resumeRepository;

        public ResumesService(IGeminiClient geminiClient, IMongoContext context)
        {
            _geminiClient = geminiClient;
            _resumeRepository = context.GetRepository<Resume>();
        }

        //will recieve the string64 encoded pdf and return the resume object (each page is summarized independently)
        public BaseResponse<Resume> getResume(string encodedPdf)
        {
            if (encodedPdf == null || encodedPdf == "")
            {
                return new BaseResponse<Resume>(ResultCodeEnum.Failed, null, "empty pdf");
            }

            PdfDocument doc = ResumesHelpers.getPdfFromString64(encodedPdf);

            string prompt = PromptHelper.addHelperToPrompt("", 2, 0);

            Resume resumeResult = new Resume();

            for (int i = 0; i < doc.Pages.Count; i++)
            { 
                Image image = doc.SaveAsImage(i, PdfImageType.Bitmap, 500, 500);

                using (MemoryStream ms2 = new MemoryStream())
                {
                    image.Save(ms2, ImageFormat.Jpeg); // Use any desired image format

                    byte[] imageBytes = ms2.ToArray();

                    string base64image = Convert.ToBase64String(imageBytes);

                    var geminiResponse = GenericGeminiClient.getImagePrompt(_geminiClient, prompt, base64image).Result;

                    if (geminiResponse != null)
                    {
                        var detailledImageContent = geminiResponse.Candidates[0].Content.Parts[0].Text;
                        var imageContentToSummarize = PromptHelper.addHelperToPrompt(detailledImageContent, 2, 0);

                        var summarizedImageContent = GenericGeminiClient.GetTextPrompt(_geminiClient, imageContentToSummarize).Result;

                        if (summarizedImageContent != null)
                        {
                            ResumesHelpers.processResumesResponse(resumeResult, summarizedImageContent);
                        }
                    }
                    else
                    {
                        return new BaseResponse<Resume>(ResultCodeEnum.Failed, null, "geminiresponse null");
                    }
                }
            }
            if (ResumesHelpers.validateResumeObject(resumeResult, doc.Pages.Count))
            {
                return new BaseResponse<Resume>(ResultCodeEnum.Success, resumeResult, "Succesfully fetched Resume");
            }
            else
            {
                return new BaseResponse<Resume>(ResultCodeEnum.Failed, null, "no pages found");
            }
        }
    }
}
