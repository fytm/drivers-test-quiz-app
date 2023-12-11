using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizAPI.Data;
using QuizAPI.DTOs;
using QuizAPI.Models;
using QuizAPI.Services;
using System.Text;

namespace QuizAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        IQuestionService _questionService;
        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Question>>> GetAllQuestions(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 5)
        {
            var questions = await _questionService.GetAllQuestions(page, pageSize);
            return (questions != null) ? Ok(questions) : NotFound();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestion(Guid id)
        {
            var question = await _questionService.GetQuestion(id);
            return (question != null) ? Ok(question) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Question>> AddQuestion(QuestionCreateDTO request)
        {
            var question = await _questionService.AddQuestion(request);
            StringBuilder stringBuilder = new StringBuilder();
            return (question != null) ? Ok(question) : BadRequest();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateQuesiton(Guid id, [FromBody] Question question)
        {
            _questionService.UpdateQuesiton(id, question);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteQuestionItem(Guid id)
        {
            _questionService.DeleteQuestion(id);
            return NoContent();
        }
    }
}
