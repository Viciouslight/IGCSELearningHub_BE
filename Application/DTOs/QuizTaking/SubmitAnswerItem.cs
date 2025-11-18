namespace IGCSELearningHub.Application.DTOs.QuizTaking
{
    public class SubmitAnswerItem
    {
        public int QuestionId { get; set; }
        public int? SelectedOptionId { get; set; } // cho MCQ là OptionId; câu khác có thể null
    }
}
