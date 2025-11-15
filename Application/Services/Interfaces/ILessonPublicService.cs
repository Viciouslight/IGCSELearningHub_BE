using Application.DTOs.Lessons;
using Application.Wrappers;

namespace Application.Services.Interfaces
{
    public interface ILessonPublicService
    {
        Task<ApiResult<IEnumerable<LessonListItemDTO>>> GetLessonsAsync(int courseId, int? accountId);
        Task<ApiResult<LessonDetailDTO>> GetLessonDetailAsync(int courseId, int lessonId, int? accountId);
    }
}

