using DotnetGeminiSDK.Client.Interfaces;
using StudyfiedBackend.Models;
using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Controllers.Gemini;
using StudyfiedBackend.DataLayer;
using PdfiumViewer;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Threading.Tasks;
using Spire.Pdf;

namespace StudyfiedBackend.Controllers.Resumes
{
    public class ResumesService : IResumesService
    {
        private readonly IGeminiClient _geminiClient;
        private readonly IMongoRepository<Resume> _resumeRepository;

        public ResumesService(IGeminiClient geminiClient, IMongoContext context)
        {
            _geminiClient = geminiClient;
            _resumeRepository = context.GetRepository<Resume>();
        }

        // Will receive the base64 encoded PDF and return the resume object
        public BaseResponse<Resume> getResume(string encodedpdf)
        {
            if (string.IsNullOrEmpty(encodedpdf))
            {
                return new BaseResponse<Resume>(ResultCodeEnum.Failed, null, "Empty PDF");
            }

            var prompt = PromptHelper.addHelperToPrompt("", 2, 0);
            string main_content = "";

            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(encodedpdf)))
            {
                using (var pdfDocument = PdfiumViewer.PdfDocument.Load(ms))
                {
                    for (int i = 0; i < pdfDocument.PageCount; i++)
                    {
                        using (var pageImage = pdfDocument.Render(i, 500, 500, true))
                        {
                            using (MemoryStream imageStream = new MemoryStream())
                            {
                                pageImage.Save(imageStream, System.Drawing.Imaging.ImageFormat.Png);
                                imageStream.Position = 0;

                                using (var image = SixLabors.ImageSharp.Image.Load<Rgba32>(imageStream))
                                {
                                    using (MemoryStream ms2 = new MemoryStream())
                                    {
                                        image.SaveAsJpeg(ms2); // Save as JPEG format
                                        byte[] imageBytes = ms2.ToArray();
                                        string base64String = Convert.ToBase64String(imageBytes);

                                        var geminiResponse = GenericGeminiClient.getImagePrompt(_geminiClient, prompt, base64String).Result;

                                        if (geminiResponse != null)
                                        {
                                            main_content += geminiResponse.Candidates[0].Content.Parts[0].Text;
                                        }
                                        else
                                        {
                                            return new BaseResponse<Resume>(ResultCodeEnum.Failed, null, "Gemini response null");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            var prompt2 = PromptHelper.addHelperToPrompt(main_content, 3, 0);
            var geminiResponse2 = GenericGeminiClient.GetTextPrompt(_geminiClient, prompt2).Result;

            if (geminiResponse2 != null)
            {
                Resume resume = ResumesHelpers.processResumesResponse(geminiResponse2);
                return new BaseResponse<Resume>(ResultCodeEnum.Success, resume, "Successfully fetched Resume");
            }

            return new BaseResponse<Resume>(ResultCodeEnum.Failed, null, "No pages found");
        }
    }
}
