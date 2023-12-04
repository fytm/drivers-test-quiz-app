using System.ComponentModel.DataAnnotations;

namespace QuizAPI.Models
{
    public class QuestionItem
    {
        [Key]
        public long Id { get; set; }
        public required String Question {get; set;}
        public required String AnswerOptions { get; set; }

    }
}
