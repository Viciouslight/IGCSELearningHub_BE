namespace IGCSELearningHub.Application.DTOs.Progress
{
    public class AdminProgressDTO
    {
        public int AccountId { get; set; }
        public string StudentName { get; set; } = null!;
        public int EnrollmentId { get; set; }
        public byte CompletedPercent { get; set; }
        public DateTime? LastAccessDate { get; set; }
    }
}
