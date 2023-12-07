using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizAPI.Data;

namespace QuizAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly DataContext _context;

        public QuestionsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Questions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestions(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
          if (_context.Questions == null)
          {
              return NotFound();
          }
            var totalQuestions = await _context.Questions.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalQuestions / pageSize);

            if (page < 1 || page > totalPages)
            {
                return BadRequest("Invalid page number");
            }

            var questions = await _context.Questions
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new
            {
                TotalQuestions = totalQuestions,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Questions = questions
            });

        }

        // GET: api/Questions/14F78495-467D-46E0-84E9-E5F83D5271D3
        [HttpGet("/{id}")]
        public async Task<ActionResult<Question>> GetQuestionItem(Guid id)
        {
            if (_context.Questions == null)
            {
                return NotFound();
            }
            var questionItem = await _context.Questions.FindAsync(id);

            if (questionItem == null)
            {
                return NotFound();
            }

            return questionItem;
        }

        // PUT: api/Questions/14F78495-467D-46E0-84E9-E5F83D5271D3
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionItem(Guid id, Question questionItem)
        {
            if (id != questionItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(questionItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Questions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Question>> PostQuestionItem([FromBody] Question questionItem)
        {
          if (_context.Questions == null)
          {
              return Problem("Entity set 'QuestionContext.Questions'  is null.");
          }
            _context.Questions.Add(questionItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetQuestionItem), new { id = questionItem.Id }, questionItem);
        }

        // DELETE: api/Questions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestionItem(int id)
        {
            if (_context.Questions == null)
            {
                return NotFound();
            }
            var questionItem = await _context.Questions.FindAsync(id);
            if (questionItem == null)
            {
                return NotFound();
            }   

            _context.Questions.Remove(questionItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionItemExists(Guid id)
        {
            return (_context.Questions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
