using IGCSELearningHub.Application.DTOs.Submissions;
using IGCSELearningHub.Application.Wrappers;

namespace IGCSELearningHub.Application.Services.Interfaces
{
    public interface IStudentSubmissionService
    {
        Task<ApiResult<SubmissionDTO>> SubmitAsync(int accountId, int assignmentId, CreateSubmissionRequest req);
        Task<PagedResult<SubmissionDTO>> GetMySubmissionsAsync(int accountId, int assignmentId, int page, int pageSize);
    }
}
