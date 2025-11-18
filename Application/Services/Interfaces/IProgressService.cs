using IGCSELearningHub.Application.DTOs.Progress;
using IGCSELearningHub.Application.Wrappers;

namespace IGCSELearningHub.Application.Services.Interfaces
{
    public interface IProgressService
    {
        Task<ApiResult<StudentProgressDTO>> GetMyProgressAsync(int accountId, int courseId);
        Task<ApiResult<StudentProgressDTO>> UpdateMyProgressAsync(int accountId, int courseId, UpdateProgressRequest req);

        Task<PagedResult<AdminProgressDTO>> GetCourseProgressAsync(int courseId, int page, int pageSize);
    }
}
