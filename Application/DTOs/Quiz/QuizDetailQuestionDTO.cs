namespace IGCSELearningHub.Application.DTOs.Quiz
{
    public class QuizDetailQuestionDTO
    {
        public int QuestionId { get; set; }
        public int? OrderIndex { get; set; }
        public decimal Points { get; set; }
        public string Stem { get; set; } = null!;
        public string? Explanation { get; set; }
        public byte? Difficulty { get; set; }
        public Domain.Enums.QuestionType Type { get; set; }
        public List<QuizDetailOptionDTO> Options { get; set; } = new();
    }
}
