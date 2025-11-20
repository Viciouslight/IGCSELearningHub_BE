using IGCSELearningHub.Domain.Payments.Enums;
using System.ComponentModel.DataAnnotations;

namespace IGCSELearningHub.Application.Payments.PaymentOrchestration.DTOs;

public sealed class CheckoutRequestDTO
{
    [Required]
    public string? ClientIp { get; set; }
    public string? BankCode { get; set; }
    public string? OrderDescription { get; set; }
    public string OrderTypeCode { get; set; } = "other";
    public PaymentChannel? Channel { get; set; }
}
