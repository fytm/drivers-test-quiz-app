using QuizAPI.DTOs;
using QuizAPI.Models;

namespace QuizAPI.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly DataContext _dataContext;

        public QuestionService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        public async Task<List<Question>> GetAllQuestions()
        {
            return await _dataContext.Questions.Include(x => x.AnswerOptions).ToListAsync();
        }

        public async Task<Question> GetQuestion(Guid id)
        {

            return await _dataContext.Questions.FindAsync(id);
        }
        public async Task<Question> AddQuestion(QuestionCreateDTO request)
        {
            Question question = new Question
            {
                Value = request.Question,
                AnswerOptions = new HashSet<Option>()
            };
            if (request.Options != null)
            {
                foreach (var value in request.Options)
                {
                    Option option = new Option
                    {
                        IsAnswer = value.IsAnswer.GetValueOrDefault(false),
                        Value = value.Value,
                        Question = question
                    };
                    question.AnswerOptions.Add(option);
                }

            }
            _dataContext.Questions.Add(question);
            await _dataContext.SaveChangesAsync();
            return await _dataContext.Questions.FindAsync(question.Id);
        }

    }
}
