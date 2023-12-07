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
            return await _dataContext.Questions.ToListAsync();
        }

        public async Task<Question> GetQuestion(Guid id)
        {

            return await _dataContext.Questions.FindAsync(id);
        }
        public async Task<Question> AddQuestion(Question question)
        {            
            Question newQuestion = new Question();
            newQuestion.AnswerOptions = question.AnswerOptions;
            newQuestion.Value = question.Value;
            _dataContext.Questions.Add(newQuestion);
            _dataContext.SaveChanges();
            return newQuestion;
        }

    }
}
