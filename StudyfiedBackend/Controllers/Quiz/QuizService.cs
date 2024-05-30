using DotnetGeminiSDK.Client.Interfaces;
using StudyfiedBackend.Models;
using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.DataLayer;
using StudyfiedBackend.DataLayer.Repositories.GenericMongoRepository;

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

        public BaseResponse<Quiz> getQuiz(string topic, string difficulty = "medium", int numberOfQuestions = 5)
        {
            try
            {
                if (topic == null || topic == "")
                {
                    return new BaseResponse<Quiz>(ResultCodeEnum.Failed, null, "Please enter a valid topic");
                }

                if (numberOfQuestions > 8 || numberOfQuestions == 0)
                {
                    return new BaseResponse<Quiz>(ResultCodeEnum.Failed, null, "Please enter a valid amount of questions: no more than 8");
                }

                Quiz quiz = new Quiz(topic, difficulty, numberOfQuestions);

                List<Question> questions = new List<Question>();
                var isValidQuestions = false;

                while (!isValidQuestions)
                {
                    Thread.Sleep(500);
                    questions = QuizHelper.GenerateQuestion(topic, difficulty, numberOfQuestions, _geminiClient);
                    isValidQuestions = QuizHelper.isValidQuestions(questions: questions, numberOfQuestions: numberOfQuestions);
                }

                foreach (Question question in questions)
                {
                    List<Answer> answers = new List<Answer>();
                    var isValidAnswers = false;

                    while (!isValidAnswers)
                    {
                        Thread.Sleep(500);
                        answers = QuizHelper.GenerateResponses(question, _geminiClient);
                        isValidAnswers = QuizHelper.isValidAnswers(answers: answers);
                    }
                    quiz.questionAnswerPairs.Add(question.content, answers);
                }
                return new BaseResponse<Quiz>(ResultCodeEnum.Success, quiz, "Successfully generated quiz");
            }
            catch (Exception ex)
            {
                // We should probably log a message here to keep track of things
                return new BaseResponse<Quiz>(ResultCodeEnum.Failed, null, $"An error occurred: Please try again in a few seconds.");
            }
        }


        public PrimitiveBaseResponse<bool> persistQuiz(Quiz quizWithUserId)
        {
            if (quizWithUserId == null)
            {
                return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Failed, false);
            }

            _quizRepository.CreateAsync(quizWithUserId);
            return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Success, true, "Succesfully added Quiz");
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
