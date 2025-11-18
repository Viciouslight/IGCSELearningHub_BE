namespace IGCSELearningHub.Application.DTOs.Progress
{
    public class StudentProgressDTO
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = null!;
        public byte CompletedPercent { get; set; }
        public DateTime? LastAccessDate { get; set; }
    }
}
