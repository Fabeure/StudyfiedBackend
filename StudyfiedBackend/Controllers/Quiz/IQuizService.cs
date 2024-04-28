using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Models;

namespace StudyfiedBackend.Controllers.Quiz
{
    public interface IQuizService
    {
        public BaseResponse<Models.Quiz> getQuiz(string topic, string difficulty = "medium", int numberOfQuestion = 5);
        public PrimitiveBaseResponse<bool> persistQuiz(Models.Quiz quizWithUserId);
        public BaseResponse<Models.Quiz> getExistingQuiz(string id);
    }
}
