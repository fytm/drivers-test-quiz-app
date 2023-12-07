using System.ComponentModel.DataAnnotations;

namespace QuizAPI.Models
{
    public class Option
    {
        [Key]
        public Guid Id { get; set; }
        public String Value { get; set; }
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }

    }
}