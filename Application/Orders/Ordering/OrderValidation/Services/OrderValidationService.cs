using IGCSELearningHub.Application.DTOs.Orders;
using IGCSELearningHub.Application.Orders.Ordering.OrderValidation.DTOs;
using IGCSELearningHub.Application.Orders.Ordering.OrderValidation.Interfaces;
using IGCSELearningHub.Application.Wrappers;
using IGCSELearningHub.Domain.Enums;
using IGCSELearningHub.Domain.Orders.Entities;
using IGCSELearningHub.Domain.Orders.Enums;
using Microsoft.EntityFrameworkCore;

namespace IGCSELearningHub.Application.Orders.Ordering.OrderValidation.Services
{
    public class OrderValidationService : IOrderValidationService
    {
        private readonly IUnitOfWork _uow;

        public OrderValidationService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<ApiResult<IReadOnlyList<ValidatedOrderItem>>> ValidateItemsAsync(int accountId, IEnumerable<OrderItemRequest> items)
        {
            var materialized = items?.ToList() ?? new List<OrderItemRequest>();
            if (materialized.Count == 0)
                return ApiResult<IReadOnlyList<ValidatedOrderItem>>.Fail("No items.", 400);

            var validated = new List<ValidatedOrderItem>();

            foreach (var it in materialized)
            {
                if (it.Quantity <= 0) return ApiResult<IReadOnlyList<ValidatedOrderItem>>.Fail("Invalid quantity.", 400);

                switch (it.ItemType)
                {
                    case ItemType.Course:
                        {
                            var alreadyEnrolled = await _uow.EnrollmentRepository.GetAllQueryable()
                                .AnyAsync(e => e.AccountId == accountId
                                               && e.CourseId == it.ItemId
                                               && !e.IsDeleted
                                               && e.Status != EnrollmentStatus.Canceled);

                            if (alreadyEnrolled)
                                return ApiResult<IReadOnlyList<ValidatedOrderItem>>.Fail($"Course #{it.ItemId} already purchased.", 400);

                            var alreadyPaidOrder = await _uow.OrderRepository
                                .GetAllQueryable($"{nameof(Order.OrderDetails)}")
                                .AnyAsync(o => o.AccountId == accountId
                                               && o.Status == OrderStatus.Paid
                                               && o.OrderDetails.Any(d => !d.IsDeleted && d.ItemType == ItemType.Course && d.ItemId == it.ItemId));

                            if (alreadyPaidOrder)
                                return ApiResult<IReadOnlyList<ValidatedOrderItem>>.Fail($"Course #{it.ItemId} already purchased.", 400);

                            var c = await _uow.CourseRepository.GetByIdAsync(it.ItemId);
                            if (c == null) return ApiResult<IReadOnlyList<ValidatedOrderItem>>.Fail($"Course #{it.ItemId} not found.", 404);
                            validated.Add(new ValidatedOrderItem { ItemType = ItemType.Course, ItemId = c.Id, UnitPrice = c.Price, Quantity = it.Quantity, Title = c.Title });
                            break;
                        }
                    case ItemType.CoursePackage:
                        {
                            var alreadyPaidPackage = await _uow.OrderRepository
                                .GetAllQueryable($"{nameof(Order.OrderDetails)}")
                                .AnyAsync(o => o.AccountId == accountId
                                               && o.Status == OrderStatus.Paid
                                               && o.OrderDetails.Any(d => !d.IsDeleted && d.ItemType == ItemType.CoursePackage && d.ItemId == it.ItemId));

                            if (alreadyPaidPackage)
                                return ApiResult<IReadOnlyList<ValidatedOrderItem>>.Fail($"Package #{it.ItemId} already purchased.", 400);

                            var p = await _uow.CoursePackageRepository.GetByIdAsync(it.ItemId);
                            if (p == null) return ApiResult<IReadOnlyList<ValidatedOrderItem>>.Fail($"Package #{it.ItemId} not found.", 404);
                            validated.Add(new ValidatedOrderItem { ItemType = ItemType.CoursePackage, ItemId = p.Id, UnitPrice = p.Price, Quantity = it.Quantity, Title = p.Name });
                            break;
                        }
                    case ItemType.Livestream:
                        {
                            var l = await _uow.LivestreamRepository.GetByIdAsync(it.ItemId);
                            if (l == null) return ApiResult<IReadOnlyList<ValidatedOrderItem>>.Fail($"Livestream #{it.ItemId} not found.", 404);
                            validated.Add(new ValidatedOrderItem { ItemType = ItemType.Livestream, ItemId = l.Id, UnitPrice = l.Price, Quantity = it.Quantity, Title = l.Title });
                            break;
                        }
                    default:
                        return ApiResult<IReadOnlyList<ValidatedOrderItem>>.Fail("Unsupported item type.", 400);
                }
            }

            return ApiResult<IReadOnlyList<ValidatedOrderItem>>.Success(validated);
        }
    }
}
