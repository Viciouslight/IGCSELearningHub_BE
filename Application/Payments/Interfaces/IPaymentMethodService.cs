using Application.DTOs.Payments;
using Application.Wrappers;

namespace Application.Payments.Interfaces
{
    public interface IPaymentMethodService
    {
        Task<ApiResult<IEnumerable<PaymentMethodDTO>>> GetAllAsync();
        Task<ApiResult<IEnumerable<PaymentMethodDTO>>> GetActiveAsync();
        Task<ApiResult<bool>> SetStatusAsync(int id, bool isActive);
    }
}
