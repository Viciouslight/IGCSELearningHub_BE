using IGCSELearningHub.Application.Payments.PaymentOrchestration.DTOs;

namespace IGCSELearningHub.Application.Payments.PaymentOrchestration.Interfaces;

public interface IPaymentGateway
{
    Task<PaymentCheckoutDTO> CreateCheckoutAsync(CreatePaymentCommand command, decimal amountVnd, CancellationToken ct = default);
}
