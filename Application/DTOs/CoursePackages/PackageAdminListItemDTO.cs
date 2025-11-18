namespace IGCSELearningHub.Application.DTOs.CoursePackages
{
    public class PackageAdminListItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int CourseCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
