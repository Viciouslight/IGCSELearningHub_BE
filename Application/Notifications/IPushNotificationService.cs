namespace IGCSELearningHub.Application.Notifications
{
    public interface IPushNotificationService
    {
        Task SendPaymentSuccessAsync(int accountId, int orderId, IEnumerable<string> deviceTokens, CancellationToken ct = default);
    }
}
