using Microsoft.AspNetCore.Mvc;
using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Controllers.FlashCards;
using StudyfiedBackend.Models;
using System;

namespace StudyfiedBackend.Controllers.Resumes
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumesController : ControllerBase
    {
        private readonly IResumesService resumesService;

        public ResumesController(IResumesService resumesService)
        {
            this.resumesService = resumesService;
        }

        [HttpPost("getResume")]
        public BaseResponse<Resume> get(string[] encodedImages)
        {
            return resumesService.getResume(encodedImages);
        }

        [HttpPost("persistResume")]
        public PrimitiveBaseResponse<bool> persistResume(Resume resumeWithUserId)
        {
            return resumesService.persistResume(resumeWithUserId);
        }

        [HttpGet("getExistingResume")]
        public BaseResponse<Resume> getExistingResume(string id)
        {
            return resumesService.getExistingResume(id);
        }

        [HttpGet("getBatchExistingResume")]
        public BaseResponse<List<Resume>> getBatchExistingResume(string[] id)
        {
            return resumesService.getBatchExistingResume(id);
        }

        [HttpGet("getResumesByUserId")]
        public BaseResponse<List<Resume>> getResumesByUserId(string userId)
        {
            return resumesService.getResumesByUserId(userId);
        }

        [HttpGet("getAllResumes")]
        public BaseResponse<List<Resume>> getAllResumes()
        {
            return resumesService.getAllResumes();
        }

        [HttpPost("updateResume")]
        public PrimitiveBaseResponse<bool> updateResume(Resume resume)
        {
            return resumesService.updateResume(resume);
        }

        [HttpGet("deleteResume")]
        public PrimitiveBaseResponse<bool> deleteResume(string id)
        {
            return resumesService.deleteResume(id);
        }

        [HttpGet("deleteBatchResume")]
        public PrimitiveBaseResponse<bool> deleteBatchResume(string[] id)
        {
            return resumesService.deleteBatchResume(id);
        }
    }
}
