using System.ComponentModel.DataAnnotations;

namespace QuizAPI.Models
{
    public class Question
    {
        [Key]
        public Guid Id { get; set; }
        public String Value { get; set; }
        //public Option Answer { get; set; }
        public HashSet<Option> AnswerOptions { get; set; } 
    }
}
