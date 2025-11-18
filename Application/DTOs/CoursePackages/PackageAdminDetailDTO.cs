namespace IGCSELearningHub.Application.DTOs.CoursePackages
{
    public class PackageAdminDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<PackageCourseItemDTO> Courses { get; set; } = new();
    }
}
