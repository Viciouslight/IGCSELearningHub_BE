namespace IGCSELearningHub.Application.DTOs.Livestreams
{
    public class LivestreamAdminDetailDTO
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
        public string Title { get; set; } = null!;
        public DateTime Schedule { get; set; }
        public decimal Price { get; set; }
        public int RegistrationCount { get; set; }
    }
}
