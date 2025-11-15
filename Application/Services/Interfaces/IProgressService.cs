using Application.DTOs.Progress;
using Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IProgressService
    {
        Task<ApiResult<StudentProgressDTO>> GetMyProgressAsync(int accountId, int courseId);
        Task<ApiResult<StudentProgressDTO>> UpdateMyProgressAsync(int accountId, int courseId, UpdateProgressRequest req);

        Task<PagedResult<AdminProgressDTO>> GetCourseProgressAsync(int courseId, int page, int pageSize);
    }
}
