using IGCSELearningHub.Application.DTOs.Lessons;
using IGCSELearningHub.Application.Wrappers;

namespace IGCSELearningHub.Application.Services.Interfaces
{
    public interface ILessonAdminService
    {
        Task<ApiResult<int>> CreateAsync(int courseId, LessonCreateDTO dto);
        Task<ApiResult<bool>> UpdateAsync(int lessonId, LessonUpdateDTO dto);
        Task<ApiResult<bool>> DeleteAsync(int lessonId);
        Task<ApiResult<bool>> UpdateOrderAsync(int lessonId, LessonOrderUpdateDTO dto);
    }
}

