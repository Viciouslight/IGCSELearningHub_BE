using Application.DTOs.Analytics;
using Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IAnalyticsService
    {
        Task<ApiResult<KpiSummaryDTO>> GetKpisAsync(DateRangeQuery q);

        Task<ApiResult<RevenueSeriesDTO>> GetRevenueSeriesAsync(DateRangeQuery q);
        Task<ApiResult<CountSeriesDTO>> GetOrdersSeriesAsync(DateRangeQuery q);
        Task<ApiResult<CountSeriesDTO>> GetEnrollmentsSeriesAsync(DateRangeQuery q);
        Task<ApiResult<CountSeriesDTO>> GetUserGrowthSeriesAsync(DateRangeQuery q);
        Task<ApiResult<RevenueSeriesDTO>> GetLivestreamRevenueSeriesAsync(DateRangeQuery q);

        Task<PagedResult<TopCourseRevenueItemDTO>> GetTopCoursesByRevenueAsync(DateRangeQuery q, int page, int pageSize);
        Task<PagedResult<TopCourseEnrollmentItemDTO>> GetTopCoursesByEnrollmentsAsync(DateRangeQuery q, int page, int pageSize);
        Task<PagedResult<TopLivestreamRevenueItemDTO>> GetTopLivestreamsByRevenueAsync(DateRangeQuery q, int page, int pageSize);
    }
}
