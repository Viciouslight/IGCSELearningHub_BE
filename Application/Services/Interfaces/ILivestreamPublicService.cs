using IGCSELearningHub.Application.DTOs.Livestreams;
using IGCSELearningHub.Application.Wrappers;

namespace IGCSELearningHub.Application.Services.Interfaces
{
    public interface ILivestreamPublicService
    {
        Task<PagedResult<LivestreamPublicListItemDTO>> GetUpcomingListAsync(int? courseId, DateTime? from, DateTime? to, string? sort, int page, int pageSize);
        Task<ApiResult<LivestreamPublicDetailDTO>> GetDetailAsync(int livestreamId);
    }
}

