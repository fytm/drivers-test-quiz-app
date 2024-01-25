using QuizAPI.Models;

namespace QuizAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        //protected override void onconfiguring(dbcontextoptionsbuilder optionsbuilder)
        //{
        //    optionsbuilder.usesqlite("data");
        //    base.onconfiguring(optionsbuilder);
        //}
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
    }
}
