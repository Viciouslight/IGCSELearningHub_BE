namespace IGCSELearningHub.Application.DTOs.Quiz
{
    public class QuizSummaryDTO
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; } = null!;
        public int TotalQuestions { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
