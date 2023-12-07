using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizAPI.Models;
using QuizAPI.Services;

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
        public async Task<ActionResult<List<Question>>> GetAllQuestions()
        {
            var questions = await _questionService.GetAllQuestions();
            return (questions != null) ? Ok(questions) : NotFound();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestion(Guid id)
        {
            Console.WriteLine("**************************Getting question*************************");
            var question = await _questionService.GetQuestion(id);
            return (question != null) ? Ok(question) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Question>> AddQuestion(Question request)
        {
            var question = await _questionService.AddQuestion(request);
            return (question != null) ? Ok(question) : BadRequest();
        }
    }
}
