using IGCSELearningHub.Application.DTOs.Assignments;
using IGCSELearningHub.Application.Wrappers;

namespace IGCSELearningHub.Application.Services.Interfaces
{
    public interface IAssignmentStudentService
    {
        Task<PagedResult<StudentAssignmentItemDTO>> GetListByCourseAsync(
            int accountId,
            int courseId,
            bool? submitted,
            int page,
            int pageSize,
            string? sort);
    }
}
