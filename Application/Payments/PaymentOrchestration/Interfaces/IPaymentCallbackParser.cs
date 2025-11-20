using IGCSELearningHub.Application.Payments.PaymentOrchestration.DTOs;
using Microsoft.AspNetCore.Http;

namespace IGCSELearningHub.Application.Payments.PaymentOrchestration.Interfaces;

public interface IPaymentCallbackParser
{
    Task<PaymentResultDTO> ParseAndValidateCallbackAsync(IQueryCollection query, CancellationToken ct = default);
}
