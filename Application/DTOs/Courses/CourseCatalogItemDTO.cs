namespace IGCSELearningHub.Application.DTOs.Courses
{
    public class CourseCatalogItemDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Level { get; set; }
        public decimal Price { get; set; }
        public string? ShortDescription { get; set; }
        public string? Info { get; set; }
        public string SubjectGroup { get; set; } = string.Empty;
        public int TotalQuizzes { get; set; }
        public int TotalAssignments { get; set; }
    }
}
