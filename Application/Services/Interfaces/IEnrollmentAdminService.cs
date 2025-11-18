using IGCSELearningHub.Application.DTOs.Enrollments;
using IGCSELearningHub.Application.Wrappers;
using IGCSELearningHub.Domain.Enums;

namespace IGCSELearningHub.Application.Services.Interfaces
{
    public interface IEnrollmentAdminService
    {
        Task<PagedResult<EnrollmentAdminListItemDTO>> GetListAsync(
            int? accountId, int? courseId, EnrollmentStatus? status, DateTime? from, DateTime? to,
            int page, int pageSize, string? sort);

        Task<ApiResult<EnrollmentAdminDetailDTO>> GetDetailAsync(int enrollmentId);
        Task<ApiResult<int>> CreateAsync(EnrollmentCreateDTO dto);          // return new EnrollmentId
        Task<ApiResult<bool>> UpdateStatusAsync(int enrollmentId, EnrollmentUpdateStatusDTO dto);
        Task<ApiResult<bool>> DeleteAsync(int enrollmentId);                // soft delete

        // Tạo enrollment từ đơn hàng đã thanh toán (idempotent)
        Task<ApiResult<int>> CreateFromOrderAsync(int orderId);             // return số enrollment được tạo
    }
}
