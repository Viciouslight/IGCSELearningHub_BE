using IGCSELearningHub.Application.DTOs.Orders;
using IGCSELearningHub.Application.Orders.Ordering.OrderValidation.DTOs;
using IGCSELearningHub.Application.Wrappers;

namespace IGCSELearningHub.Application.Orders.Ordering.OrderValidation.Interfaces
{
    public interface IOrderValidationService
    {
        Task<ApiResult<IReadOnlyList<ValidatedOrderItem>>> ValidateItemsAsync(int accountId, IEnumerable<OrderItemRequest> items);
    }
}
