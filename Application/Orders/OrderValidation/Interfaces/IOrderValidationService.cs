using IGCSELearningHub.Application.DTOs.Orders;
using IGCSELearningHub.Application.Orders.OrderValidation.DTOs;
using IGCSELearningHub.Application.Wrappers;

namespace IGCSELearningHub.Application.Orders.OrderValidation.Interfaces
{
    public interface IOrderValidationService
    {
        Task<ApiResult<IReadOnlyList<ValidatedOrderItem>>> ValidateItemsAsync(int accountId, IEnumerable<OrderItemRequest> items);
    }
}
