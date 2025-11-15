using Application.DTOs.CoursePackages;
using Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ICoursePackageAdminService
    {
        Task<PagedResult<PackageAdminListItemDTO>> GetListAsync(string? q, string? sort, int page, int pageSize);
        Task<ApiResult<PackageAdminDetailDTO>> GetDetailAsync(int packageId);
        Task<ApiResult<int>> CreateAsync(PackageCreateDTO dto);          // return new PackageId
        Task<ApiResult<bool>> UpdateAsync(int packageId, PackageUpdateDTO dto);
        Task<ApiResult<bool>> DeleteAsync(int packageId);

        Task<ApiResult<bool>> AddCoursesAsync(int packageId, PackageAddCoursesDTO dto);
        Task<ApiResult<bool>> RemoveCourseAsync(int packageId, int courseId);
    }
}
