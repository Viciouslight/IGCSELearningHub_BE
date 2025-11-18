using IGCSELearningHub.Domain.Enums;

namespace IGCSELearningHub.Application.DTOs.Enrollments
{
    public class MyEnrollmentDetailDTO
    {
        public int EnrollmentId { get; set; }
        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = null!;
        public string? CourseLevel { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public EnrollmentStatus Status { get; set; }
        public byte? CompletedPercent { get; set; }
        public DateTime? LastAccessDate { get; set; }
    }
}
