using IGCSELearningHub.Application.Payments.DTOs;
using Microsoft.AspNetCore.Http;

namespace IGCSELearningHub.Application.Payments.Interfaces
{
    public interface IPaymentGateway
    {
        Task<PaymentResultDTO> ParseAndValidateCallbackAsync(IQueryCollection query, CancellationToken ct = default);
    }
}
