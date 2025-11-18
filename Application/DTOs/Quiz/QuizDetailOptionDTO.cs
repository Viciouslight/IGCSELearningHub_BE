namespace IGCSELearningHub.Application.DTOs.Quiz
{
    public class QuizDetailOptionDTO
    {
        public int OptionId { get; set; }
        public string Text { get; set; } = null!;
        public bool IsCorrect { get; set; }   // CHỈ hiển thị ở API quản trị
    }
}
