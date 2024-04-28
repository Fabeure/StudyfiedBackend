using Microsoft.AspNetCore.Mvc;
using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Models;

namespace StudyfiedBackend.Controllers.Quiz
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
            public BaseResponse<Models.Quiz> get(string topic, string difficulty)
            {
                return quizService.getQuiz(topic, difficulty);
            }

            [HttpPost("persistQuiz")]
            public PrimitiveBaseResponse<bool> persistQuiz(Models.Quiz quizWithUserId)
            {
                return quizService.persistQuiz(quizWithUserId);
            }

            [HttpGet("getExistingQuiz")]
            public BaseResponse<Models.Quiz> getExistingQuiz(string id)
            {
                return quizService.getExistingQuiz(id);
            }

        }
    }
