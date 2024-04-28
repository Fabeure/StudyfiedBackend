using DotnetGeminiSDK.Client.Interfaces;
using StudyfiedBackend.Models;
using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.DataLayer;

namespace StudyfiedBackend.Controllers.Quize
{
    public class QuizService : IQuizService
    {
        private readonly IGeminiClient _geminiClient;
        private readonly IMongoRepository<Quiz> _quizRepository;

        public QuizService(IGeminiClient geminiClient, IMongoContext context)
        {
            _geminiClient = geminiClient;
            _quizRepository = context.GetRepository<Quiz>();
        }
        public BaseResponse<Quiz> getQuiz(string topic, string difficulty="medium", int numberOfQuestion=5)
        {
            if (topic == null || topic == "" )  
            {
                return new BaseResponse<Quiz>(ResultCodeEnum.Failed, null);
            }

            Quiz quiz = new Quiz(topic, difficulty, numberOfQuestion);

            List<Question> questions = QuizHelper.GenerateQuestion(topic, difficulty,numberOfQuestion,_geminiClient);

            foreach (Question question in questions)
            {
                if (question != null)
                {
                    List<Response> responses = QuizHelper.GenerateResponses(question, _geminiClient);
                    quiz.quizQuestions.Add(question, responses);
                }
            }
            return new BaseResponse<Quiz>(ResultCodeEnum.Success, quiz, "Succesfull");
        }
        public PrimitiveBaseResponse<bool> persistQuiz(Quiz quizWithUserId)
        {
            if (quizWithUserId == null)
            {
                return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Failed, false);
            }

            _quizRepository.CreateAsync(quizWithUserId);
            return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Success, true, "Succesfully persisted Quiz");
        }
        public BaseResponse<Quiz> getExistingQuiz(string id)
        {
            if (id == null || id == "")
            {
                return new BaseResponse<Quiz>(ResultCodeEnum.Failed, null);
            }

            Quiz quiz = _quizRepository.GetByIdAsync(id).Result;
            if (quiz == null)
            {
                return new BaseResponse<Quiz>(ResultCodeEnum.Failed, null);
            }

            return new BaseResponse<Quiz>(ResultCodeEnum.Success, quiz, "Succesfully fetched Quiz");
        }

    }
}
