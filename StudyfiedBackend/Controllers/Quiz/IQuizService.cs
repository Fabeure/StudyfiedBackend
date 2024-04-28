using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Models;

namespace StudyfiedBackend.Controllers.Quize
{
    public interface IQuizService
    {
        public BaseResponse<Quiz> getQuiz(string topic, string difficulty = "medium", int numberOfQuestion = 5);
        public PrimitiveBaseResponse<bool> persistQuiz(Quiz quizWithUserId);
        public BaseResponse<Quiz> getExistingQuiz(string id);
    }
}
