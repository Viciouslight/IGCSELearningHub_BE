namespace IGCSELearningHub.Application.DTOs.QuizTaking
{
    public class SubmitAttemptDTO
    {
        public List<SubmitAnswerItem> Answers { get; set; } = new();
    }
}
