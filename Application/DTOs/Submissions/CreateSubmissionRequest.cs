namespace IGCSELearningHub.Application.DTOs.Submissions
{
    public class CreateSubmissionRequest
    {
        public string AttachmentUrl { get; set; } = null!;
        public string? TextAnswer { get; set; }
    }
}
