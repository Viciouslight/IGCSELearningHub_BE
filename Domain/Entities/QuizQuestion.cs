using IGCSELearningHub.Domain.Common;

namespace IGCSELearningHub.Domain.Entities;

public partial class QuizQuestion : BaseFullEntity
{
    public int QuizId { get; set; }

    public int QuestionId { get; set; }

    public decimal Points { get; set; }

    public int? OrderIndex { get; set; }

    public virtual Question Question { get; set; } = null!;

    public virtual Quiz Quiz { get; set; } = null!;
}
