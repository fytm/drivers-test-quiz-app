namespace QuizAPI.DTOs
{
    public record struct OptionCreateDTO
    (
        String Value,
        bool ?IsAnswer,
        Guid ?QuestionId
    );
}
