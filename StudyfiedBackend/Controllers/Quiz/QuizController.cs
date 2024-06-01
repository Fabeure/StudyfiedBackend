using Microsoft.AspNetCore.Mvc;
using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Models;

namespace StudyfiedBackend.Controllers.Quize
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService quizService;

        public QuizController(IQuizService quizService)
        {
            this.quizService = quizService;
        }

        [HttpPost("getQuiz")]
        public BaseResponse<Quiz> get(string topic, string difficulty, int numberOfQuestion)
        {
            return quizService.getQuiz(topic, difficulty, numberOfQuestion);
        }

        [HttpPost("persistQuiz")]
        public PrimitiveBaseResponse<bool> persistQuiz(Quiz quizWithUserId)
        {
            return quizService.persistQuiz(quizWithUserId);
        }

        [HttpGet("getExistingQuiz")]
        public BaseResponse<Quiz> getExistingQuiz(string id)
        {
            return quizService.getExistingQuiz(id);
        }

        [HttpGet("getBatchExistingQuiz")]
        public BaseResponse<List<Quiz>> getBatchExistingQuiz(string[] id)
        {
            return quizService.getBatchExistingQuiz(id);
        }

        [HttpGet("getQuizzesByUserId")]
        public BaseResponse<List<Quiz>> getQuizzesByUserId(string userId)
        {
            return quizService.getQuizzesByUserId(userId);
        }

        [HttpGet("getAllQuizzes")]
        public BaseResponse<List<Quiz>> getAllQuizzes()
        {
            return quizService.getAllQuizzes();
        }

        [HttpPost("updateQuiz")]
        public PrimitiveBaseResponse<bool> updateQuiz(Quiz quiz)
        {
            return quizService.updateQuiz(quiz);
        }

        [HttpGet("deleteQuiz")]
        public PrimitiveBaseResponse<bool> deleteQuiz(string id)
        {
            return quizService.deleteQuiz(id);
        }

        [HttpGet("deleteBatchQuiz")]
        public PrimitiveBaseResponse<bool> deleteBatchQuiz(string[] id)
        {
            return quizService.deleteBatchQuiz(id);
        }

    }
}
