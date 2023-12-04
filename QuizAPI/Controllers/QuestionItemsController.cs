using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizAPI.Models;

namespace QuizAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionItemsController : ControllerBase
    {
        private readonly QuestionContext _context;

        public QuestionItemsController(QuestionContext context)
        {
            _context = context;
        }

        // GET: api/QuestionItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionItem>>> GetQuestions(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
          if (_context.QuestionItems == null)
          {
              return NotFound();
          }
            var totalQuestions = await _context.QuestionItems.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalQuestions / pageSize);

            if (page < 1 || page > totalPages)
            {
                return BadRequest("Invalid page number");
            }

            var questions = await _context.QuestionItems
                .OrderBy(q => q.Id) // Order by a suitable property
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

        // GET: api/QuestionItems/5
        [HttpGet("/{id}")]
        public async Task<ActionResult<QuestionItem>> GetQuestionItem(int id)
        {
            if (_context.QuestionItems == null)
            {
                return NotFound();
            }
            var questionItem = await _context.QuestionItems.FindAsync(id);

            if (questionItem == null)
            {
                return NotFound();
            }

            return questionItem;
        }

        // PUT: api/QuestionItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionItem(int id, QuestionItem questionItem)
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

        // POST: api/QuestionItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<QuestionItem>> PostQuestionItem([FromBody] QuestionItem questionItem)
        {
          if (_context.QuestionItems == null)
          {
              return Problem("Entity set 'QuestionContext.QuestionItems'  is null.");
          }
            _context.QuestionItems.Add(questionItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetQuestionItem), new { id = questionItem.Id }, questionItem);
        }

        // DELETE: api/QuestionItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestionItem(int id)
        {
            if (_context.QuestionItems == null)
            {
                return NotFound();
            }
            var questionItem = await _context.QuestionItems.FindAsync(id);
            if (questionItem == null)
            {
                return NotFound();
            }   

            _context.QuestionItems.Remove(questionItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionItemExists(int id)
        {
            return (_context.QuestionItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
