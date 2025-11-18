using IGCSELearningHub.Application.DTOs.Enrollments;
using IGCSELearningHub.Application.Extensions;
using IGCSELearningHub.Application.Services.Interfaces;
using IGCSELearningHub.Application.Wrappers;
using IGCSELearningHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IGCSELearningHub.Application.Services
{
    public class EnrollmentStudentService : IEnrollmentStudentService
    {
        private readonly IUnitOfWork _uow;

        public EnrollmentStudentService(IUnitOfWork uow) => _uow = uow;

        public async Task<PagedResult<MyEnrollmentItemDTO>> GetMyEnrollmentsAsync(int accountId, int page, int pageSize)
        {
            var query = _uow.EnrollmentRepository
                .GetAllQueryable($"{nameof(Enrollment.Course)},{nameof(Enrollment.Progresses)}")
                .Where(e => e.AccountId == accountId);
            query = query.OrderByDescending(e => e.EnrollmentDate);
            return await query.ToPagedResultAsync(page, pageSize, e => new MyEnrollmentItemDTO
            {
                EnrollmentId = e.Id,
                CourseId = e.CourseId,
                CourseTitle = e.Course.Title,
                EnrollmentDate = e.EnrollmentDate,
                Status = e.Status,
                CompletedPercent = e.Progresses.OrderByDescending(p => p.ModifiedAt)
                    .Select(p => (byte?)p.CompletedPercent).FirstOrDefault()
            });
        }

        public async Task<ApiResult<MyEnrollmentDetailDTO>> GetMyEnrollmentDetailAsync(int accountId, int enrollmentId)
        {
            var e = await _uow.EnrollmentRepository.GetAllQueryable(
                    $"{nameof(Enrollment.Course)},{nameof(Enrollment.Progresses)}")
                .FirstOrDefaultAsync(x => x.Id == enrollmentId && x.AccountId == accountId);

            if (e == null) return ApiResult<MyEnrollmentDetailDTO>.Fail("Enrollment not found.", 404);

            var dto = new MyEnrollmentDetailDTO
            {
                EnrollmentId = e.Id,
                CourseId = e.CourseId,
                CourseTitle = e.Course.Title,
                CourseLevel = e.Course.Level,
                EnrollmentDate = e.EnrollmentDate,
                Status = e.Status,
                CompletedPercent = e.Progresses.OrderByDescending(p => p.ModifiedAt).Select(p => (byte?)p.CompletedPercent).FirstOrDefault(),
                LastAccessDate = e.Progresses.OrderByDescending(p => p.LastAccessDate).Select(p => p.LastAccessDate).FirstOrDefault()
            };

            return ApiResult<MyEnrollmentDetailDTO>.Success(dto);
        }
    }
}
