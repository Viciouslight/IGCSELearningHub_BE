using IGCSELearningHub.Application.Payments.PaymentProcessing.DTOs;
using IGCSELearningHub.Application.Wrappers;

namespace IGCSELearningHub.Application.Payments.PaymentProcessing.Interfaces
{
    public interface ICashPaymentService
    {
        Task<ApiResult<CashPaymentResultDTO>> ProcessAsync(int orderId, CashPaymentRequestDTO request);
    }
}
