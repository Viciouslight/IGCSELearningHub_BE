using IGCSELearningHub.Application.DTOs.Courses;
using IGCSELearningHub.Application.Extensions;
using IGCSELearningHub.Application.Services.Interfaces;
using IGCSELearningHub.Application.Wrappers;
using IGCSELearningHub.Domain.Entities;
using IGCSELearningHub.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IGCSELearningHub.Application.Services
{
    public class CourseAdminService : ICourseAdminService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<CourseAdminService> _logger;

        public CourseAdminService(IUnitOfWork uow, ILogger<CourseAdminService> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<PagedResult<CourseAdminListItemDTO>> GetListAsync(
            string? q, string? level, int page, int pageSize, string? sort)
        {
            var query = _uow.CourseRepository.GetAllQueryable();

            if (!string.IsNullOrWhiteSpace(q))
            {
                var key = q.Trim().ToLower();
                query = query.Where(c => c.Title.ToLower().Contains(key) ||
                                         (c.Description != null && c.Description.ToLower().Contains(key)));
            }
            if (!string.IsNullOrWhiteSpace(level))
            {
                var lv = level.Trim();
                query = query.Where(c => c.Level == lv);
            }

            query = sort?.ToLower() switch
            {
                "price_asc" => query.OrderBy(c => c.Price),
                "price_desc" => query.OrderByDescending(c => c.Price),
                "title_desc" => query.OrderByDescending(c => c.Title),
                "title_asc" => query.OrderBy(c => c.Title),
                "createdat_asc" => query.OrderBy(c => c.CreatedAt),
                _ => query.OrderByDescending(c => c.CreatedAt)
            };

            return await query.ToPagedResultAsync(page, pageSize, c => new CourseAdminListItemDTO
            {
                Id = c.Id,
                Title = c.Title,
                Level = c.Level,
                SubjectGroup = c.SubjectGroup.ToString(),
                Price = c.Price,
                CreatedAt = c.CreatedAt
            });
        }

        public async Task<ApiResult<CourseAdminDetailDTO>> GetDetailAsync(int courseId)
        {
            var c = await _uow.CourseRepository.GetAllQueryable()
                .Include(x => x.Quizzes)
                .Include(x => x.Assignments)
                .Include(x => x.Livestreams)
                .Include(x => x.Enrollments)
                .FirstOrDefaultAsync(x => x.Id == courseId);

            if (c == null) return ApiResult<CourseAdminDetailDTO>.Fail("Course not found.", 404);

            var dto = new CourseAdminDetailDTO
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Info = c.Info,
                Level = c.Level,
                SubjectGroup = c.SubjectGroup.ToString(),
                Price = c.Price,
                CreatedAt = c.CreatedAt,
                TotalQuizzes = c.Quizzes.Count(x => !x.IsDeleted),
                TotalAssignments = c.Assignments.Count(x => !x.IsDeleted),
                TotalLivestreams = c.Livestreams.Count(x => !x.IsDeleted),
                TotalEnrollments = c.Enrollments.Count(x => !x.IsDeleted)
            };

            return ApiResult<CourseAdminDetailDTO>.Success(dto);
        }

        public async Task<ApiResult<int>> CreateAsync(CourseCreateDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                return ApiResult<int>.Fail("Title is required.", 400);
            if (dto.Price < 0)
                return ApiResult<int>.Fail("Price must be >= 0.", 400);

            if (!TryParseSubjectGroup(dto.SubjectGroup, out var subjectGroup, out var groupError))
                return ApiResult<int>.Fail(groupError ?? "Invalid subject group.", 400);

            var course = new Course
            {
                Title = dto.Title.Trim(),
                Description = dto.Description,
                Info = dto.Info,
                Level = dto.Level,
                SubjectGroup = subjectGroup,
                Price = dto.Price
            };

            await _uow.CourseRepository.AddAsync(course);
            await _uow.SaveChangesAsync();

            return ApiResult<int>.Success(course.Id, "Created", 201);
        }

        public async Task<ApiResult<bool>> UpdateAsync(int courseId, CourseUpdateDTO dto)
        {
            var course = await _uow.CourseRepository.GetByIdAsync(courseId);
            if (course == null) return ApiResult<bool>.Fail("Course not found.", 404);

            if (string.IsNullOrWhiteSpace(dto.Title))
                return ApiResult<bool>.Fail("Title is required.", 400);
            if (dto.Price < 0)
                return ApiResult<bool>.Fail("Price must be >= 0.", 400);

            if (!TryParseSubjectGroup(dto.SubjectGroup, out var subjectGroup, out var groupError))
                return ApiResult<bool>.Fail(groupError ?? "Invalid subject group.", 400);

            course.Title = dto.Title.Trim();
            course.Description = dto.Description;
            course.Info = dto.Info;
            course.Level = dto.Level;
            course.SubjectGroup = subjectGroup;
            course.Price = dto.Price;

            _uow.CourseRepository.Update(course);
            await _uow.SaveChangesAsync();

            return ApiResult<bool>.Success(true, "Updated", 200);
        }

        public async Task<ApiResult<bool>> DeleteAsync(int courseId)
        {
            var course = await _uow.CourseRepository.GetByIdAsync(courseId);
            if (course == null) return ApiResult<bool>.Fail("Course not found.", 404);

            _uow.CourseRepository.SoftDelete(course);
            await _uow.SaveChangesAsync();

            return ApiResult<bool>.Success(true, "Deleted", 200);
        }

        private static bool TryParseSubjectGroup(string? input, out SubjectGroup group, out string? errorMessage)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                group = SubjectGroup.None;
                errorMessage = null;
                return true;
            }

            if (Enum.TryParse<SubjectGroup>(input.Trim(), true, out group))
            {
                errorMessage = null;
                return true;
            }

            group = SubjectGroup.None;
            errorMessage = $"SubjectGroup '{input}' is invalid. Allowed values: {string.Join(", ", Enum.GetNames(typeof(SubjectGroup)))}.";
            return false;
        }
    }
}
