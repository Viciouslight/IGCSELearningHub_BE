namespace IGCSELearningHub.Application.DTOs.Assignments
{
    public class AssignmentCreateDTO
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
    }
}
