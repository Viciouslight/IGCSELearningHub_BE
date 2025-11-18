using IGCSELearningHub.Domain.Common;

namespace IGCSELearningHub.Domain.Entities;

public partial class Quiz : BaseFullEntity
{
    public int CourseId { get; set; }

    public string Title { get; set; } = null!;

    public int? TotalQuestions { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<QuizAttempt> QuizAttempts { get; set; } = new List<QuizAttempt>();

    public virtual ICollection<QuizQuestion> QuizQuestions { get; set; } = new List<QuizQuestion>();
}
