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
        public BaseResponse<Resume> get([FromBody] string encodedpdf)
        {
            return resumesService.getResume(encodedpdf);
        }
    }
}
