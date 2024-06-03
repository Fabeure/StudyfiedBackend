using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Models;

namespace StudyfiedBackend.Controllers.Resumes
{
    public interface IResumesService
    {
        public BaseResponse<Resume> getResume(string[] encodedImages, string token);
        public PrimitiveBaseResponse<bool> persistResume(Resume resumeWithUserId);
        public BaseResponse<Resume> getExistingResume(string id);
        public BaseResponse<List<Resume>> getBatchExistingResume(string[] id);
        public BaseResponse<List<Resume>> getResumesByUserId(string userId);
        public BaseResponse<List<Resume>> getAllResumes();
        public PrimitiveBaseResponse<bool> deleteResume(string id);
        public PrimitiveBaseResponse<bool> updateResume(Resume updatedResume);
        public PrimitiveBaseResponse<bool> deleteBatchResume(string[] id);

    }
}
