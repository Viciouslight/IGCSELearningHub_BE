using IGCSELearningHub.Application.DTOs.Units;
using IGCSELearningHub.Application.Wrappers;

namespace IGCSELearningHub.Application.Services.Interfaces
{
    public interface IUnitService
    {
        Task<ApiResult<IEnumerable<UnitDTO>>> GetByCourseAsync(int courseId);
        Task<ApiResult<UnitDTO>> GetByIdAsync(int unitId);
        Task<ApiResult<int>> CreateAsync(int courseId, UnitCreateDTO dto);
        Task<ApiResult<bool>> UpdateAsync(int unitId, UnitUpdateDTO dto);
        Task<ApiResult<bool>> DeleteAsync(int unitId);
    }
}
