using IGCSELearningHub.Application.Payments.DTOs;

namespace IGCSELearningHub.Application.Payments.Interfaces;

public interface IPaymentGateway
{
    Task<PaymentCheckoutDTO> CreateCheckoutAsync(CreatePaymentCommand command, decimal amountVnd, CancellationToken ct = default);
}
