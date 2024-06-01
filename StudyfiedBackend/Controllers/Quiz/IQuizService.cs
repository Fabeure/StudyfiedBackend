using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Models;

namespace StudyfiedBackend.Controllers.Quize
{
    public interface IQuizService
    {
        public BaseResponse<Quiz> getQuiz(string topic, string difficulty = "medium", int numberOfQuestion = 5);
        public PrimitiveBaseResponse<bool> persistQuiz(Quiz quizWithUserId);
        public BaseResponse<Quiz> getExistingQuiz(string id);
        public BaseResponse<List<Quiz>> getBatchExistingQuiz(string[] id);
        public BaseResponse<List<Quiz>> getQuizzesByUserId(string userId);
        public BaseResponse<List<Quiz>> getAllQuizzes();
        public PrimitiveBaseResponse<bool> deleteQuiz(string id);
        public PrimitiveBaseResponse<bool> updateQuiz(Quiz updatedQuiz);
        public PrimitiveBaseResponse<bool> deleteBatchQuiz(string[] id);
    }
}
