using IGCSELearningHub.Domain.Enums;

namespace IGCSELearningHub.Application.DTOs.Enrollments
{
    public class EnrollmentAdminListItemDTO
    {
        public int EnrollmentId { get; set; }
        public int AccountId { get; set; }
        public string AccountUserName { get; set; } = null!;
        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = null!;
        public DateTime EnrollmentDate { get; set; }
        public EnrollmentStatus Status { get; set; }
    }
}
