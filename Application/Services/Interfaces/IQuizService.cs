using IGCSELearningHub.Application.DTOs.Quiz;
using IGCSELearningHub.Application.Wrappers;

namespace IGCSELearningHub.Application.Services.Interfaces
{
    public interface IQuizService
    {
        Task<PagedResult<QuizSummaryDTO>> GetListAsync(int? courseId, int page, int pageSize);
        Task<ApiResult<QuizDetailDTO>> GetDetailAsync(int quizId);
        Task<ApiResult<QuizDetailDTO>> CreateAsync(QuizCreateDTO dto);
        Task<ApiResult<QuizDetailDTO>> UpdateAsync(int quizId, QuizUpdateDTO dto);
        Task<ApiResult<bool>> DeleteAsync(int quizId);
    }
}
