namespace IGCSELearningHub.Application.DTOs.Analytics
{
    public class TopCourseEnrollmentItemDTO
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = null!;
        public int Enrollments { get; set; }
    }
}
