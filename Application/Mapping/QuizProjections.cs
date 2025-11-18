using IGCSELearningHub.Application.DTOs.Quiz;
using IGCSELearningHub.Domain.Entities;
using System.Linq.Expressions;

namespace IGCSELearningHub.Application.Mapping
{
    public static class QuizProjections
    {
        public static readonly Expression<Func<Quiz, QuizSummaryDTO>> Summary =
            quiz => new QuizSummaryDTO
            {
                Id = quiz.Id,
                CourseId = quiz.CourseId,
                Title = quiz.Title,
                TotalQuestions = quiz.TotalQuestions ?? 0,
                CreatedAt = quiz.CreatedAt
            };
    }
}
