using Application.Payments.DTOs;
using Application.Wrappers;

namespace Application.Payments.Interfaces
{
    public interface ICashPaymentService
    {
        Task<ApiResult<CashPaymentResultDTO>> ProcessAsync(int orderId, CashPaymentRequestDTO request);
    }
}
