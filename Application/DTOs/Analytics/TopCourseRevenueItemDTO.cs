namespace IGCSELearningHub.Application.DTOs.Analytics
{
    public class TopCourseRevenueItemDTO
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = null!;
        public decimal Revenue { get; set; }
        public int Orders { get; set; }
    }
}
