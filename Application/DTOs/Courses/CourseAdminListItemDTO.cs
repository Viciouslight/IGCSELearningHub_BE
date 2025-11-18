namespace IGCSELearningHub.Application.DTOs.Courses
{
    public class CourseAdminListItemDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Level { get; set; }
        public string SubjectGroup { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
