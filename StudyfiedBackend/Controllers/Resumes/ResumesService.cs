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

        //will recieve the string64 encoded pdf and return the resume object
        public BaseResponse<Resume> getResume(string encodedpdf)
        {
            if (encodedpdf == null || encodedpdf == "")
            {
                return new BaseResponse<Resume>(ResultCodeEnum.Failed, null,"empty pdf");
            }

            var prompt = PromptHelper.addHelperToPrompt("", 2, 0);

            string main_content = "";

            MemoryStream ms = new MemoryStream(Convert.FromBase64String(encodedpdf));

            PdfDocument doc = new PdfDocument();

            doc.LoadFromStream(ms);

            for (int i = 0; i < doc.Pages.Count; i++)
            { 
                Image image = doc.SaveAsImage(i, PdfImageType.Bitmap, 500, 500);

                using (MemoryStream ms2 = new MemoryStream())
                {
                    image.Save(ms2, ImageFormat.Jpeg); // Use any desired image format

                    byte[] imageBytes = ms2.ToArray();

                    string base64String = Convert.ToBase64String(imageBytes);

                    var geminiResponse = GenericGeminiClient.getImagePrompt(_geminiClient, prompt, base64String).Result;

                    if (geminiResponse != null)
                        {
                            main_content = main_content + geminiResponse.Candidates[0].Content.Parts[0].Text;
                        }

                    else
                        {
                            return new BaseResponse<Resume>(ResultCodeEnum.Failed, null,"geminiresponse null");
                        }
                }
            }

            var prompt2 = PromptHelper.addHelperToPrompt(main_content, 3, 0);

            var geminiResponse2 = GenericGeminiClient.GetTextPrompt(_geminiClient, prompt2).Result;

            if (geminiResponse2 != null)
            {
                Resume resume = ResumesHelpers.processResumesResponse(geminiResponse2);

                return new BaseResponse<Resume>(ResultCodeEnum.Success, resume, "Succesfully fetched Resume");
            }


            return new BaseResponse<Resume>(ResultCodeEnum.Failed, null, "no pages found");

            /* var prompt = PromptHelper.addHelperToPrompt("", 2, 0); 

             var geminiResponse = GenericGeminiClient.getImagePrompt(_geminiClient, prompt , pdfdecode).Result;
            */


        }       
}
}
