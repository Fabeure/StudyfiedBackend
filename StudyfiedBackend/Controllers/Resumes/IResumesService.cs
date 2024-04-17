using Microsoft.AspNetCore.Mvc;
using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Models;

namespace StudyfiedBackend.Controllers.Resumes
{
    public interface IResumesService
    {
        public BaseResponse<Resume> getResume(string encodedpdf);
    

    }
}
