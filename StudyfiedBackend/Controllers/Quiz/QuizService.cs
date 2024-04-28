using DotnetGeminiSDK.Client.Interfaces;
using StudyfiedBackend.Models;
using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Controllers.Gemini;
using StudyfiedBackend.DataLayer;

namespace StudyfiedBackend.Controllers.Quiz
{
    public class QuizService : IQuizService
    {
        private readonly IGeminiClient _geminiClient;
        private readonly IMongoRepository<Models.Quiz> _quizRepository;

        public QuizService(IGeminiClient geminiClient, IMongoContext context)
        {
            _geminiClient = geminiClient;
            _quizRepository = context.GetRepository<Models.Quiz>();
        }
        public BaseResponse<Models.Quiz> getQuiz(string topic, string difficulty="medium", int numberOfQuestion=5)
        {
            if (topic == null || topic == "" )  
            {
                return new BaseResponse<Models.Quiz>(ResultCodeEnum.Failed, null);
            }
            Models.Quiz quiz = new Models.Quiz(topic,difficulty,numberOfQuestion);
            List<Question> questions = QuizHelper.GenerateQuestion(topic, difficulty,numberOfQuestion,_geminiClient);
            foreach (Question question in questions)
            {

                if (question != null)
                {
                    List<Response> responses = QuizHelper.GenerateResponse(question,_geminiClient);
                    quiz.quizQuestions.Add(question, responses);
                }
                
            }
            return new BaseResponse<Models.Quiz>(ResultCodeEnum.Success, quiz, "Succesfull");
        }
        public PrimitiveBaseResponse<bool> persistQuiz(Models.Quiz quizWithUserId)
        {
            if (quizWithUserId == null)
            {
                return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Failed, false);
            }

            _quizRepository.CreateAsync(quizWithUserId);
            return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Success, true, "Succesfully persisted Quiz");
        }
        public BaseResponse<Models.Quiz> getExistingQuiz(string id)
        {
            if (id == null || id == "")
            {
                return new BaseResponse<Models.Quiz>(ResultCodeEnum.Failed, null);
            }

            Models.Quiz quiz = _quizRepository.GetByIdAsync(id).Result;
            if (quiz == null)
            {
                return new BaseResponse<Models.Quiz>(ResultCodeEnum.Failed, null);
            }

            return new BaseResponse<Models.Quiz>(ResultCodeEnum.Success, quiz, "Succesfully fetched Quiz");
        }

    }
}
