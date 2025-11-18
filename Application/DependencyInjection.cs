using IGCSELearningHub.Application.Identity;
using IGCSELearningHub.Application.Payments.Interfaces;
using IGCSELearningHub.Application.Payments.Services;
using IGCSELearningHub.Application.Services;
using IGCSELearningHub.Application.Services.Interfaces;
using IGCSELearningHub.Application.Utils;
using IGCSELearningHub.Application.Utils.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IGCSELearningHub.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies());

            services.AddIdentityModule();
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();

            services.AddScoped<IQuizService, QuizService>();
            services.AddScoped<IQuizTakingService, QuizTakingService>();
            services.AddScoped<ICourseCatalogService, CourseCatalogService>();
            services.AddScoped<ICourseAdminService, CourseAdminService>();
            services.AddScoped<IAssignmentAdminService, AssignmentAdminService>();
            services.AddScoped<ILivestreamAdminService, LivestreamAdminService>();
            services.AddScoped<ICoursePackageAdminService, CoursePackageAdminService>();
            services.AddScoped<IEnrollmentAdminService, EnrollmentAdminService>();
            services.AddScoped<ILessonAdminService, LessonAdminService>();
            services.AddScoped<IEnrollmentStudentService, EnrollmentStudentService>();
            services.AddScoped<IAssignmentStudentService, AssignmentStudentService>();
            services.AddScoped<ILessonStudentService, LessonStudentService>();
            services.AddScoped<IUnitService, UnitService>();
            services.AddScoped<IStudentSubmissionService, StudentSubmissionService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderQueryService, OrderQueryService>();
            services.AddScoped<IProgressService, ProgressService>();
            services.AddScoped<IAnalyticsService, AnalyticsService>();
            services.AddScoped<ILivestreamPublicService, LivestreamPublicService>();
            services.AddScoped<ICoursePackagePublicService, CoursePackagePublicService>();
            services.AddScoped<ILessonPublicService, LessonPublicService>();
            services.AddScoped<IPaymentMethodService, PaymentMethodService>();

            return services;
        }
    }
}
