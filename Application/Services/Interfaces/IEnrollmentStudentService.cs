using IGCSELearningHub.Application.DTOs.Enrollments;
using IGCSELearningHub.Application.Wrappers;

namespace IGCSELearningHub.Application.Services.Interfaces
{
    public interface IEnrollmentStudentService
    {
        Task<PagedResult<MyEnrollmentItemDTO>> GetMyEnrollmentsAsync(int accountId, int page, int pageSize);
        Task<ApiResult<MyEnrollmentDetailDTO>> GetMyEnrollmentDetailAsync(int accountId, int enrollmentId);
    }
}
