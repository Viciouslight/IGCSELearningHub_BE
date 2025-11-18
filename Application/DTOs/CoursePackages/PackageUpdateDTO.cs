namespace IGCSELearningHub.Application.DTOs.CoursePackages
{
    public class PackageUpdateDTO
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public List<int>? CourseIds { get; set; } // optional: replace-all khi cập nhật
        public bool ReplaceAllCourses { get; set; } = false; // true -> thay toàn bộ
    }
}
