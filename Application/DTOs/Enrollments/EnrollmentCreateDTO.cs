using IGCSELearningHub.Domain.Enums;

namespace IGCSELearningHub.Application.DTOs.Enrollments
{
    public class EnrollmentCreateDTO
    {
        public int AccountId { get; set; }
        public int CourseId { get; set; }
        public EnrollmentStatus Status { get; set; } = EnrollmentStatus.Active;
    }
}
