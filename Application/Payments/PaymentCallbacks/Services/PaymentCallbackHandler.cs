using IGCSELearningHub.Application.Payments.PaymentCallbacks.Interfaces;
using IGCSELearningHub.Application.Payments.PaymentOrchestration.DTOs;
using IGCSELearningHub.Application.Payments.PaymentOrchestration.Interfaces;
using Microsoft.AspNetCore.Http;

namespace IGCSELearningHub.Application.Payments.PaymentCallbacks.Services;

public class PaymentCallbackHandler : IPaymentCallbackHandler
{
    private readonly IPaymentCallbackParser _gateway;

    public PaymentCallbackHandler(IPaymentCallbackParser gateway)
    {
        _gateway = gateway;
    }

    public Task<PaymentResultDTO> HandleAsync(IQueryCollection query, CancellationToken ct = default)
        => _gateway.ParseAndValidateCallbackAsync(query, ct);
}
