using IGCSELearningHub.Application.Utils.Interfaces;

namespace IGCSELearningHub.Application.Utils
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
