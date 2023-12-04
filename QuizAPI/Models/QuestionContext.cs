using Microsoft.EntityFrameworkCore;

namespace QuizAPI.Models
{
    public class QuestionContext:DbContext
    {
        public QuestionContext(DbContextOptions<QuestionContext> options) : base(options) {
        }
        public DbSet<QuestionItem> QuestionItems { get; set; } = null!;

    }
}
