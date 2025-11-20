using IGCSELearningHub.Application.Payments.DTOs;
using Microsoft.AspNetCore.Http;

namespace IGCSELearningHub.Application.Payments.Interfaces
{
    public interface IPaymentGateway
    {
        Task<PaymentCheckoutDTO> CreateCheckoutAsync(CreatePaymentCommand command, decimal amountVnd, CancellationToken ct = default);
        Task<PaymentResultDTO> ParseAndValidateCallbackAsync(IQueryCollection query, CancellationToken ct = default);
    }
}
