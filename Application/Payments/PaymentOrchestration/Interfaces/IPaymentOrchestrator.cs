using IGCSELearningHub.Application.Payments.PaymentOrchestration.DTOs;
using Microsoft.AspNetCore.Http;

namespace IGCSELearningHub.Application.Payments.PaymentOrchestration.Interfaces;

public interface IPaymentOrchestrator
{
    Task<PaymentCheckoutDTO> CreateCheckoutAsync(CreatePaymentCommand command, CancellationToken ct = default);
    Task<PaymentResultDTO> HandleCallbackAsync(IQueryCollection query, CancellationToken ct = default);
}
