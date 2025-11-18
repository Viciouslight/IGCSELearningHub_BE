namespace IGCSELearningHub.Application.DTOs.Progress
{
    public class ProgressDTO
    {
        public int ProgressId { get; set; }
        public int EnrollmentId { get; set; }
        public byte CompletedPercent { get; set; }
        public DateTime? LastAccessDate { get; set; }
    }
}
