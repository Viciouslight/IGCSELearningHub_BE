using IGCSELearningHub.Application.DTOs.CoursePackages;
using IGCSELearningHub.Application.Wrappers;

namespace IGCSELearningHub.Application.Services.Interfaces
{
    public interface ICoursePackagePublicService
    {
        Task<PagedResult<PackagePublicListItemDTO>> GetListAsync(string? q, string? sort, int page, int pageSize);
        Task<ApiResult<PackagePublicDetailDTO>> GetDetailAsync(int packageId);
    }
}

