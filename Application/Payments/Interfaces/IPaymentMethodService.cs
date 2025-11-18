using IGCSELearningHub.Application.DTOs.Payments;
using IGCSELearningHub.Application.Wrappers;

namespace IGCSELearningHub.Application.Payments.Interfaces
{
    public interface IPaymentMethodService
    {
        Task<ApiResult<IEnumerable<PaymentMethodDTO>>> GetAllAsync();
        Task<ApiResult<IEnumerable<PaymentMethodDTO>>> GetActiveAsync();
        Task<ApiResult<bool>> SetStatusAsync(int id, bool isActive);
    }
}
