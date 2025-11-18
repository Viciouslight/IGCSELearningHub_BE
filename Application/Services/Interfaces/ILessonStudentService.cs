using IGCSELearningHub.Application.DTOs.Courses;
using IGCSELearningHub.Application.DTOs.Lessons;
using IGCSELearningHub.Application.Wrappers;

namespace IGCSELearningHub.Application.Services.Interfaces
{
    public interface ILessonStudentService
    {
        Task<ApiResult<CourseDetailDTO>> GetMyCourseDetailAsync(int accountId, int courseId);
        Task<ApiResult<IEnumerable<LessonDetailDTO>>> GetMyLessonsAsync(int accountId, int courseId);
        Task<ApiResult<LessonDetailDTO>> GetMyLessonDetailAsync(int accountId, int courseId, int lessonId);
        Task<ApiResult<object>> CompleteLessonAsync(int accountId, int courseId, int lessonId);
    }
}
