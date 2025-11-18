using IGCSELearningHub.Domain.Common;

namespace IGCSELearningHub.Domain.Entities;

public partial class LessonCompletion : BaseFullEntity
{
    public int EnrollmentId { get; set; }
    public Enrollment Enrollment { get; set; } = null!;

    public int LessonId { get; set; }
    public Lesson Lesson { get; set; } = null!;

    public DateTime CompletedAt { get; set; }
}

