using QuizAPI.Models;

namespace QuizAPI.DTOs
{
    public record struct QuestionCreateDTO
    (
        string Question,
        List<OptionCreateDTO> ?Options
    );
}
