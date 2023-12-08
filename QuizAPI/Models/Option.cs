using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QuizAPI.Models
{
    public class Option
    {
        [Key]
        public Guid Id { get; set; }
        public String Value { get; set; } = String.Empty;
        public bool IsAnswer { get; set; } = false;
        [JsonIgnore]
        public Question Question { get; set; }

    }
}