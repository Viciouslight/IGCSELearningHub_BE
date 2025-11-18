using IGCSELearningHub.Application.DTOs.Courses;
using IGCSELearningHub.Application.Wrappers;

namespace IGCSELearningHub.Application.Services.Interfaces
{
    public interface ICourseCatalogService
    {
        Task<PagedResult<CourseCatalogItemDTO>> GetCatalogAsync(
            string? q, string? level, decimal? priceMin, decimal? priceMax,
            int page, int pageSize, string? sort);

        Task<ApiResult<CourseDetailDTO>> GetDetailAsync(int courseId);
    }
}
