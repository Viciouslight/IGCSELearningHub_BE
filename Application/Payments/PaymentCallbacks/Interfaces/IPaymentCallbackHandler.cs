using IGCSELearningHub.Application.Payments.PaymentOrchestration.DTOs;
using Microsoft.AspNetCore.Http;

namespace IGCSELearningHub.Application.Payments.PaymentCallbacks.Interfaces;

public interface IPaymentCallbackHandler
{
    Task<PaymentResultDTO> HandleAsync(IQueryCollection query, CancellationToken ct = default);
}
