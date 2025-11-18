namespace IGCSELearningHub.Application.DTOs.Quiz
{
    public class QuizDetailDTO : QuizSummaryDTO
    {
        public bool RandomizeQuestions { get; set; }
        public bool RandomizeOptions { get; set; }
        public List<QuizDetailQuestionDTO> Questions { get; set; } = new();
    }
}
