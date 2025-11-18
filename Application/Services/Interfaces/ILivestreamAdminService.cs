using IGCSELearningHub.Application.DTOs.Livestreams;
using IGCSELearningHub.Application.Wrappers;

namespace IGCSELearningHub.Application.Services.Interfaces
{
    public interface ILivestreamAdminService
    {
        Task<PagedResult<LivestreamAdminListItemDTO>> GetListAsync(
            int? courseId, string? q, DateTime? from, DateTime? to, string? sort, int page, int pageSize);

        Task<ApiResult<LivestreamAdminDetailDTO>> GetDetailAsync(int livestreamId);
        Task<ApiResult<int>> CreateAsync(LivestreamCreateDTO dto);          // return new Id
        Task<ApiResult<bool>> UpdateAsync(int livestreamId, LivestreamUpdateDTO dto);
        Task<ApiResult<bool>> DeleteAsync(int livestreamId);

        Task<PagedResult<LivestreamRegistrationListItemDTO>> GetRegistrationsAsync(
            int livestreamId, string? paymentStatus, int page, int pageSize);

        Task<ApiResult<bool>> UpdateRegistrationPaymentStatusAsync(int registrationId, UpdateRegistrationPaymentStatusDTO dto);
    }
}
