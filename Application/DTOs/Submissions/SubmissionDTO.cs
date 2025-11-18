namespace IGCSELearningHub.Application.DTOs.Submissions
{
    public class SubmissionDTO
    {
        public int SubmissionId { get; set; }
        public int AssignmentId { get; set; }
        public int AccountId { get; set; }
        public decimal? Score { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string? AttachmentUrl { get; set; }
        public string? TextAnswer { get; set; }
    }
}
