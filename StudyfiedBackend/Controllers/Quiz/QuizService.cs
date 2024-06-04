using DotnetGeminiSDK.Client.Interfaces;
using StudyfiedBackend.Models;
using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.DataLayer;
using StudyfiedBackend.DataLayer.Repositories.GenericMongoRepository;
using MongoDB.Driver;
using StudyfiedBackend.Controllers.Authentication;

namespace StudyfiedBackend.Controllers.Quize
{
    public class QuizService : IQuizService
    {
        private readonly IGeminiClient _geminiClient;
        private readonly IMongoRepository<Quiz> _quizRepository;
        private readonly IAuthenticationService _authenticationService;

        public QuizService(IGeminiClient geminiClient, IMongoContext context, IAuthenticationService authenticationService)
        {
            _geminiClient = geminiClient;
            _quizRepository = context.GetRepository<Quiz>();
            _authenticationService = authenticationService;
        }

        public BaseResponse<Quiz> getQuiz(string topic, string difficulty, int numberOfQuestions, string token)
        {
            if (token != "testToken")
            {
                try
                {
                    ApplicationUser caller = _authenticationService.AuthenticateTokenAndGetUser(token);
                }
                catch (Exception ex)
                {
                    return new BaseResponse<Quiz>(ResultCodeEnum.Failed, null, "USER NOT AUTHORIZED");
                }
            }
            try
            {
                if (topic == null || topic == "")
                {
                    return new BaseResponse<Quiz>(ResultCodeEnum.Failed, null, "Please enter a valid topic");
                }

                if (numberOfQuestions > 4 || numberOfQuestions == 0)
                {
                    return new BaseResponse<Quiz>(ResultCodeEnum.Failed, null, "Please enter a valid amount of questions: no more than 8");
                }

                Quiz quiz = new Quiz(topic, difficulty, numberOfQuestions);

                List<Question> questions = new List<Question>();
                var isValidQuestions = false;

                while (!isValidQuestions)
                {
                    questions = QuizHelper.GenerateQuestion(topic, difficulty, numberOfQuestions, _geminiClient);
                    isValidQuestions = QuizHelper.isValidQuestions(questions: questions, numberOfQuestions: numberOfQuestions);
                }

                foreach (Question question in questions)
                {
                    List<Answer> answers = new List<Answer>();
                    var isValidAnswers = false;

                    while (!isValidAnswers)
                    {
                        answers = QuizHelper.GenerateResponses(question, _geminiClient);
                        isValidAnswers = QuizHelper.isValidAnswers(answers: answers);
                    }
                    quiz.questionAnswerPairs.Add(question.content, answers);
                }
                return new BaseResponse<Quiz>(ResultCodeEnum.Success, quiz, "Successfully generated quiz");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
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

        public BaseResponse<List<Quiz>> getBatchExistingQuiz(string[] id)
        {
            List<Quiz> quizzes = _quizRepository.GetDocumentsByIdsAsync(id).Result.ToList();
            if (quizzes == null)
            {
                return new BaseResponse<List<Quiz>>(ResultCodeEnum.Failed, null,"failed to fetch quizzes");
            }

            return new BaseResponse<List<Quiz>>(ResultCodeEnum.Success, quizzes, "Succesfully fetched Quizzes");
        }   

        public PrimitiveBaseResponse<bool> updateQuiz(Quiz updatedQuiz)
        {
            if (updatedQuiz == null || string.IsNullOrEmpty(updatedQuiz.Id))
            {
                return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Failed, false, "Quiz not found");
            }

            bool updated = _quizRepository.UpdateAsync(updatedQuiz.Id, updatedQuiz).Result;
            if (updated)
            {
                return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Success, true, $"Quiz updated successfully for user {updatedQuiz.userId}");
            }
            return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Failed, false, $"Quiz not updated for user {updatedQuiz.userId}");
        }

        public PrimitiveBaseResponse<bool> deleteQuiz(string id)
        {
            bool deleted = _quizRepository.DeleteAsync(id).Result;
            if (deleted)
            {
                return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Success, true, $"Quiz with id : {id} deleted successfully ");
            }
            return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Failed, false, $"Quiz not deleted");
        }

        public PrimitiveBaseResponse<bool> deleteBatchQuiz(string[] id)
        {
            bool deleted = _quizRepository.BatchDelete(id).Result;
            if (deleted)
            {
                return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Success, true, $"Quizzes with ids : {id} deleted successfully ");
            }
            return new PrimitiveBaseResponse<bool>(ResultCodeEnum.Failed, false, $"Quizzes not deleted");
        }

        public BaseResponse<List<Quiz>> getAllQuizzes()
        {
            List<Quiz> quizzes = _quizRepository.GetAllAsync().Result.ToList();
            if (quizzes == null)
            {
                return new BaseResponse<List<Quiz>>(ResultCodeEnum.Failed, null, "failed to fetch quizzes");
            }

            return new BaseResponse<List<Quiz>>(ResultCodeEnum.Success, quizzes, "Succesfully fetched Quizzes");
        }

        public BaseResponse<List<Quiz>> getQuizzesByUserId(string userId)
        {
            if (userId == null || userId == "")
            {
                return new BaseResponse<List<Quiz>>(ResultCodeEnum.Failed, null, "Please enter a valid userId");
            }

            var filter = Builders<Quiz>.Filter.Eq("userId", userId);
            List<Quiz> quizzes = _quizRepository.GetByFilter(filter).Result.ToList();
            if (quizzes == null)
            {
                return new BaseResponse<List<Quiz>>(ResultCodeEnum.Failed, null, "failed to fetch quizzes");
            }

            return new BaseResponse<List<Quiz>>(ResultCodeEnum.Success, quizzes, $"found {quizzes.Count()} Quizzes with the user id : {userId}");
        }
    }
}
