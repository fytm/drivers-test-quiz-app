namespace QuizAPI.DTOs
{
    public class QuestionDTO
    {
        public string Question { get; set; }
        public HashSet<Option> AnswerOptions { get; set; }
    }
}
