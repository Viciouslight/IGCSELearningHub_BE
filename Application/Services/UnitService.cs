using IGCSELearningHub.Application.DTOs.Units;
using IGCSELearningHub.Application.Services.Interfaces;
using IGCSELearningHub.Application.Wrappers;
using IGCSELearningHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IGCSELearningHub.Application.Services
{
    public class UnitService : IUnitService
    {
        private readonly IUnitOfWork _uow;
        public UnitService(IUnitOfWork uow) => _uow = uow;

        public async Task<ApiResult<IEnumerable<UnitDTO>>> GetByCourseAsync(int courseId)
        {
            var exists = await _uow.CourseRepository.GetAllQueryable()
                .AnyAsync(c => c.Id == courseId && !c.IsDeleted);
            if (!exists) return ApiResult<IEnumerable<UnitDTO>>.Fail("Course not found.", 404);

            var data = await _uow.UnitRepository.GetAllQueryable()
                .Where(unit => unit.CourseId == courseId)
                .OrderBy(unit => unit.OrderIndex)
                .Select(unit => new UnitDTO
                {
                    UnitId = unit.Id,
                    CourseId = unit.CourseId,
                    Title = unit.Title,
                    Description = unit.Description,
                    OrderIndex = unit.OrderIndex
                })
                .ToListAsync();

            return ApiResult<IEnumerable<UnitDTO>>.Success(data);
        }

        public async Task<ApiResult<int>> CreateAsync(int courseId, UnitCreateDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                return ApiResult<int>.Fail("Title is required.", 400);

            var courseExists = await _uow.CourseRepository.GetAllQueryable()
                .AnyAsync(c => c.Id == courseId && !c.IsDeleted);
            if (!courseExists) return ApiResult<int>.Fail("Course not found.", 404);

            var existingOrder = await _uow.UnitRepository.GetAllQueryable()
                .Where(unit => unit.CourseId == courseId)
                .Select(unit => (int?)unit.OrderIndex)
                .MaxAsync() ?? 0;

            var desiredOrder = dto.OrderIndex.GetValueOrDefault(existingOrder + 1);
            if (desiredOrder <= 0) desiredOrder = existingOrder + 1;

            var unit = new Unit
            {
                CourseId = courseId,
                Title = dto.Title.Trim(),
                Description = dto.Description,
                OrderIndex = desiredOrder
            };

            await _uow.UnitRepository.AddAsync(unit);
            await _uow.SaveChangesAsync();

            return ApiResult<int>.Success(unit.Id, "Unit created.", 201);
        }

        public async Task<ApiResult<UnitDTO>> GetByIdAsync(int unitId)
        {
            var unit = await _uow.UnitRepository.GetByIdAsync(unitId);
            if (unit == null)
                return ApiResult<UnitDTO>.Fail("Unit not found.", 404);

            var dto = new UnitDTO
            {
                UnitId = unit.Id,
                CourseId = unit.CourseId,
                Title = unit.Title,
                Description = unit.Description,
                OrderIndex = unit.OrderIndex
            };

            return ApiResult<UnitDTO>.Success(dto);
        }

        public async Task<ApiResult<bool>> UpdateAsync(int unitId, UnitUpdateDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                return ApiResult<bool>.Fail("Title is required.", 400);

            var unit = await _uow.UnitRepository.GetByIdAsync(unitId);
            if (unit == null)
                return ApiResult<bool>.Fail("Unit not found.", 404);

            unit.Title = dto.Title.Trim();
            unit.Description = dto.Description;
            if (dto.OrderIndex.HasValue && dto.OrderIndex.Value > 0)
            {
                unit.OrderIndex = dto.OrderIndex.Value;
            }

            _uow.UnitRepository.Update(unit);
            await _uow.SaveChangesAsync();

            return ApiResult<bool>.Success(true, "Unit updated.", 200);
        }

        public async Task<ApiResult<bool>> DeleteAsync(int unitId)
        {
            var unit = await _uow.UnitRepository.GetByIdAsync(unitId);
            if (unit == null)
                return ApiResult<bool>.Fail("Unit not found.", 404);

            _uow.UnitRepository.SoftDelete(unit);
            await _uow.SaveChangesAsync();

            return ApiResult<bool>.Success(true, "Unit deleted.", 200);
        }
    }
}
