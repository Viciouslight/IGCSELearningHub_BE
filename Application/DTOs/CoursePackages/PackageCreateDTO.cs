namespace IGCSELearningHub.Application.DTOs.CoursePackages
{
    public class PackageCreateDTO
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public List<int>? CourseIds { get; set; } // optional: add courses ngay khi tạo
    }
}
