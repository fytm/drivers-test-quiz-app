using Microsoft.AspNetCore.Mvc;
using QuizAPI.DTOs;
using QuizAPI.Models;

namespace QuizAPI.Services
{
    public interface IQuestionService
    {
        Task<List<Question>> GetAllQuestions();
        Task<Question> GetQuestion(Guid id);
        Task<Question> AddQuestion(QuestionCreateDTO questionCreateDTO);
    }
}