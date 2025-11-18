using IGCSELearningHub.Domain.Common;
using IGCSELearningHub.Domain.Identity.Accounts;

namespace IGCSELearningHub.Domain.Entities;

public partial class QuizAttempt : BaseFullEntity
{
    public int QuizId { get; set; }

    public int AccountId { get; set; }

    public decimal? Score { get; set; }

    public DateTime AttemptDate { get; set; }

    public virtual ICollection<AttemptAnswer> AttemptAnswers { get; set; } = new List<AttemptAnswer>();

    public virtual Quiz Quiz { get; set; } = null!;

    public virtual Account Account { get; set; } = null!;
}
