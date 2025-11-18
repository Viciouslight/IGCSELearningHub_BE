namespace IGCSELearningHub.Application.DTOs.Assignments
{
    public class SubmissionDetailDTO
    {
        public int SubmissionId { get; set; }
        public int AssignmentId { get; set; }
        public int AccountId { get; set; }
        public string? AccountUserName { get; set; }
        public decimal? Score { get; set; }
        public DateTime SubmittedDate { get; set; }
    }
}
