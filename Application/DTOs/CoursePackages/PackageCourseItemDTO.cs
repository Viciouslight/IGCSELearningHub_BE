namespace IGCSELearningHub.Application.DTOs.CoursePackages
{
    public class PackageCourseItemDTO
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = null!;
        public string? Level { get; set; }
        public decimal Price { get; set; }
    }
}
