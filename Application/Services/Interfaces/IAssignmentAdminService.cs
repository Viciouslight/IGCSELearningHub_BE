using Application.DTOs.Assignments;
using Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
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

