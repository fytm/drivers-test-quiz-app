using Microsoft.AspNetCore.Mvc;
using QuizAPI.DTOs;
using QuizAPI.Models;

namespace QuizAPI.Services
{
    public interface IQuestionService
    {
        Task<List<Question>> GetAllQuestions(int page, int pageSize);
        Task<Question> GetQuestion(Guid id);
        Task<Question> AddQuestion(QuestionCreateDTO questionCreateDTO);
        void UpdateQuesiton(Guid id, Question question);
        void DeleteQuestion(Guid id);

    }
}