using DotnetGeminiSDK.Client.Interfaces;
using StudyfiedBackend.Models;
using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Controllers.Gemini;
using StudyfiedBackend.DataLayer;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System.Drawing;
using System.Drawing.Imaging;
using StudyfiedBackend.DataLayer.Repositories.GenericMongoRepository;
using MongoDB.Driver;


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

            if (doc.Pages.Count == 0)
            {
                return new BaseResponse<Resume>(ResultCodeEnum.Failed, null, "no pages found");
            }

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
                        return new BaseResponse<Resume>(ResultCodeEnum.Failed, null, "gemini response is null");
                    }
                }
            }
            if (ResumesHelpers.validateResumeObject(resumeResult, doc.Pages.Count))
            {
                return new BaseResponse<Resume>(ResultCodeEnum.Success, resumeResult, "Succesfully fetched Resume");
            }
            else
            {
                return getResume(encodedPdf: encodedPdf);
            }
        }

        public PrimitiveBaseResponse<bool> persistResume(Resume resumeWithUserId)
        {
            Resume addedResume = _resumeRepository.CreateAsync(resumeWithUserId).Result;
            if (addedResume != null)
            {
                return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Success, true, $"Resume added successfully for user {resumeWithUserId.UserId}");
            }
            return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Failed, false, $"Resume not added for user {resumeWithUserId.UserId}");
        }

        public BaseResponse<Resume> getExistingResume(string id)
        {
            Resume resume = _resumeRepository.GetByIdAsync(id).Result;
            if (resume != null)
            {
                return new BaseResponse<Resume>(ResultCodeEnum.Success, resume, $"Successfully fetched resume {resume.Id}");
            }
            return new BaseResponse<Resume>(ResultCodeEnum.Failed, null, $"No resume with id {id} found");
        }

        public BaseResponse<List<Resume>> getBatchExistingResume(string[] id)
        {
            List<Resume> resumes = _resumeRepository.GetDocumentsByIdsAsync(id).Result.ToList();
            if (resumes != null)
            {
                return new BaseResponse<List<Resume>>(ResultCodeEnum.Success, resumes, "resumes fetched");
            }
            return new BaseResponse<List<Resume>>(ResultCodeEnum.Failed, null, "failed to fetch resumes");
        }

        public PrimitiveBaseResponse<bool> updateResume(Resume updatedResume)
        {
            if (updatedResume == null || string.IsNullOrEmpty(updatedResume.Id))
            {
                return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Failed, false, "Resume not found");
            }
            bool updated = _resumeRepository.UpdateAsync(updatedResume.Id, updatedResume).Result;
            if (updated)
            {
                return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Success, true, $"Resume updated successfully for user {updatedResume.UserId}");
            }
            return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Failed, false, $"Resume not updated for user {updatedResume.UserId}");
        }

        public PrimitiveBaseResponse<bool> deleteResume(string id)
        {
            bool deleted = _resumeRepository.DeleteAsync(id).Result;
            if (deleted)
            {
                return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Success, true, $"Resume with id : {id} deleted successfully");
            }
            return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Failed, false, $"Resume not deleted");
        }

        public PrimitiveBaseResponse<bool> deleteBatchResume(string[] id)
        {
            bool deleted = _resumeRepository.BatchDelete(id).Result;
            if (deleted)
            {
                return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Success, true, $"Resumes with ids : {id} deleted successfully");
            }
            return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Failed, false, $"Resumes not deleted");
        }

        public BaseResponse<List<Resume>> getAllResumes()
        {
            List<Resume> resumes = _resumeRepository.GetAllAsync().Result.ToList();
            return new BaseResponse<List<Resume>>(ResultCodeEnum.Success, resumes, "resumes fetched!");
        }

        public BaseResponse<List<Resume>> getResumesByUserId(string userId)
        {
            var filter = Builders<Resume>.Filter.Eq("userId", userId);
            List<Resume> resumes = _resumeRepository.GetByFilter(filter).Result.ToList();

            if (resumes != null)
            {
                return new BaseResponse<List<Resume>>(ResultCodeEnum.Success, resumes, $"found {resumes.Count()} resumes with user id : {userId}");
            }
            return new BaseResponse<List<Resume>>(ResultCodeEnum.Failed, null, "failed to fetch resumes");
        }
    }
}
