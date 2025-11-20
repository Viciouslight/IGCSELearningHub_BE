using IGCSELearningHub.Application;
using IGCSELearningHub.Application.DTOs.Orders;
using IGCSELearningHub.Application.Extensions;
using IGCSELearningHub.Application.Orders.OrderPlacement.DTOs;
using IGCSELearningHub.Application.Orders.OrderPlacement.Interfaces;
using IGCSELearningHub.Application.Orders.OrderPricing.Interfaces;
using IGCSELearningHub.Application.Utils.Interfaces;
using IGCSELearningHub.Application.Wrappers;
using IGCSELearningHub.Domain.Enums;
using IGCSELearningHub.Domain.Orders.Entities;
using IGCSELearningHub.Domain.Orders.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IGCSELearningHub.Application.Orders.OrderPlacement.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDateTimeProvider _clock;
        private readonly IOrderPricingService _pricing;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IUnitOfWork uow, IDateTimeProvider clock, IOrderPricingService pricing, ILogger<OrderService> logger)
        {
            _uow = uow;
            _clock = clock;
            _pricing = pricing;
            _logger = logger;
        }

        public async Task<ApiResult<OrderSummaryDTO>> CreateOrderAsync(int accountId, CreateOrderRequest req)
        {
            if (req.Items == null || req.Items.Count == 0)
                return ApiResult<OrderSummaryDTO>.Fail("No items.", 400);

            var quoteResult = await _pricing.BuildPriceQuoteAsync(accountId, req.Items);
            if (!quoteResult.Succeeded) return ApiResult<OrderSummaryDTO>.Fail(quoteResult.Message ?? "Pricing failed.", quoteResult.StatusCode);
            var quote = quoteResult.Data!;

            var order = new Order
            {
                AccountId = accountId,
                OrderDate = _clock.UtcNow,
                TotalAmount = quote.TotalAmount,
                Status = OrderStatus.Pending
            };
            await _uow.OrderRepository.AddAsync(order);
            await _uow.SaveChangesAsync();

            foreach (var line in quote.Items)
            {
                var detail = new OrderDetail
                {
                    OrderId = order.Id,
                    ItemType = line.ItemType,
                    ItemId = line.ItemId,
                    Price = line.UnitPrice
                };
                await _uow.OrderDetailRepository.AddAsync(detail);
            }
            await _uow.SaveChangesAsync();

            // Build response
            var dto = await BuildOrderSummaryAsync(order.Id, accountId);
            return ApiResult<OrderSummaryDTO>.Success(dto, "OrderIndex created.", 201);
        }

        public async Task<ApiResult<OrderSummaryDTO>> GetOrderAsync(int accountId, int orderId)
        {
            var exists = await _uow.OrderRepository.GetAllQueryable()
                .AnyAsync(o => o.Id == orderId && o.AccountId == accountId);
            if (!exists) return ApiResult<OrderSummaryDTO>.Fail("OrderIndex not found.", 404);

            var dto = await BuildOrderSummaryAsync(orderId, accountId);
            return ApiResult<OrderSummaryDTO>.Success(dto);
        }

        public async Task<PagedResult<MyOrderListItemDTO>> GetMyOrdersAsync(int accountId, int page, int pageSize)
        {
            var q = _uow.OrderRepository.GetAllQueryable()
                .Where(o => o.AccountId == accountId);

            q = q.OrderByDescending(o => o.OrderDate);
            return await q.ToPagedResultAsync(page, pageSize, o => new MyOrderListItemDTO
            {
                OrderId = o.Id,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                Status = o.Status.ToString(),
                Lines = o.OrderDetails.Count(x => !x.IsDeleted)
            });
        }

        // -------- helper --------
        private async Task<OrderSummaryDTO> BuildOrderSummaryAsync(int orderId, int accountId)
        {
            var o = await _uow.OrderRepository.GetAllQueryable(
                    $"{nameof(Order.OrderDetails)}")
                .FirstAsync(x => x.Id == orderId && x.AccountId == accountId);

            // map titles by type
            var lines = new List<OrderDetailLineDTO>();
            foreach (var d in o.OrderDetails.Where(x => !x.IsDeleted))
            {
                string title;
                switch (d.ItemType)
                {
                    case ItemType.Course:
                        var c = await _uow.CourseRepository.GetByIdAsync(d.ItemId);
                        title = c?.Title ?? $"Course #{d.ItemId}";
                        break;
                    case ItemType.CoursePackage:
                        var p = await _uow.CoursePackageRepository.GetByIdAsync(d.ItemId);
                        title = p?.Name ?? $"Package #{d.ItemId}";
                        break;
                    case ItemType.Livestream:
                        var l = await _uow.LivestreamRepository.GetByIdAsync(d.ItemId);
                        title = l?.Title ?? $"Livestream #{d.ItemId}";
                        break;
                    default: title = $"Item #{d.ItemId}"; break;
                }

                lines.Add(new OrderDetailLineDTO
                {
                    OrderDetailId = d.Id,
                    ItemType = d.ItemType,
                    ItemId = d.ItemId,
                    Title = title,
                    UnitPrice = d.Price,
                    Quantity = 1,                 // nếu muốn lưu Quantity, thêm field vào OrderDetail
                    LineTotal = d.Price * 1
                });
            }

            return new OrderSummaryDTO
            {
                OrderId = o.Id,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                Status = o.Status.ToString(),
                Items = lines
            };
        }
    }
}
