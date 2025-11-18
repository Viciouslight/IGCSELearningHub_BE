using IGCSELearningHub.Application.Payments.DTOs;
using IGCSELearningHub.Application.Wrappers;

namespace IGCSELearningHub.Application.Payments.Interfaces
{
    public interface ICashPaymentService
    {
        Task<ApiResult<CashPaymentResultDTO>> ProcessAsync(int orderId, CashPaymentRequestDTO request);
    }
}
