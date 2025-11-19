using IGCSELearningHub.Application.Payments.PaymentMethods.DTOs;
using IGCSELearningHub.Application.Wrappers;

namespace IGCSELearningHub.Application.Payments.PaymentMethods.Interfaces
{
    public interface IPaymentMethodService
    {
        Task<ApiResult<IEnumerable<PaymentMethodDTO>>> GetAllAsync();
        Task<ApiResult<IEnumerable<PaymentMethodDTO>>> GetActiveAsync();
        Task<ApiResult<bool>> SetStatusAsync(int id, bool isActive);
    }
}
