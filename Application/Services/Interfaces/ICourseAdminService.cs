using Application.DTOs.Courses;
using Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ICourseAdminService
    {
        Task<PagedResult<CourseAdminListItemDTO>> GetListAsync(
            string? q, string? level, int page, int pageSize, string? sort);

        Task<ApiResult<CourseAdminDetailDTO>> GetDetailAsync(int courseId);
        Task<ApiResult<int>> CreateAsync(CourseCreateDTO dto);       // return new Id
        Task<ApiResult<bool>> UpdateAsync(int courseId, CourseUpdateDTO dto);
        Task<ApiResult<bool>> DeleteAsync(int courseId);
    }
}
