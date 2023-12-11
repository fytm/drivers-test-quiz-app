using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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

        private async Task<Question> findQuestionById(Guid id)
        {
            var question = await _dataContext.Questions.FindAsync(id);
            if (question == null)
            {
                throw new Exception($"Question {id} is not found");
            }
            return question;
        }

        public async Task<List<Question>> GetAllQuestions(
            int page,
            int pageSize)
        {
            var totalQuestions = await _dataContext.Questions.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalQuestions / pageSize);

            if (page < 1 || page > totalPages)
            {
                throw new Exception("Invalid page number");
            }
            var questions = await _dataContext.Questions.Include(x => x.AnswerOptions)
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return questions;
        }

        public async Task<Question> GetQuestion(Guid id)
        {

            Question question = await findQuestionById(id);
            return question;
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
            return await findQuestionById(question.Id);
        }

        public async void UpdateQuesiton(Guid id, Question question)
        {
            _dataContext.Entry(question).State = EntityState.Modified;
            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var updatedQuestion = findQuestionById(id);
                throw;
            }
        }

        public async void DeleteQuestion(Guid id)
        {
            Question question = await findQuestionById(id);
            _dataContext.Questions.Remove(question);
            await _dataContext.SaveChangesAsync();
        }

    }
}
