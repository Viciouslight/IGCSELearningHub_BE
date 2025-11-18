using IGCSELearningHub.Application.DTOs.Assignments;
using IGCSELearningHub.Application.Wrappers;

namespace IGCSELearningHub.Application.Services.Interfaces
{
    public interface IAssignmentAdminService
    {
        Task<PagedResult<AssignmentAdminListItemDTO>> GetListAsync(int? courseId, string? q, int page, int pageSize, string? sort);
        Task<ApiResult<AssignmentAdminDetailDTO>> GetDetailAsync(int assignmentId);
        Task<ApiResult<int>> CreateAsync(AssignmentCreateDTO dto); // return new AssignmentId
        Task<ApiResult<bool>> UpdateAsync(int assignmentId, AssignmentUpdateDTO dto);
        Task<ApiResult<bool>> DeleteAsync(int assignmentId);

        // Submissions (gio vin xem/ch?m)
        Task<PagedResult<SubmissionListItemDTO>> GetSubmissionsAsync(int assignmentId, int page, int pageSize);
        Task<ApiResult<SubmissionDetailDTO>> GetSubmissionDetailAsync(int submissionId);
        Task<ApiResult<bool>> GradeSubmissionAsync(int submissionId, GradeSubmissionDto dto);
    }
}

