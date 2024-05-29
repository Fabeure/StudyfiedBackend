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

    }
}
