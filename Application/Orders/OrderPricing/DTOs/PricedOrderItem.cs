using IGCSELearningHub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGCSELearningHub.Application.Orders.OrderPricing.DTOs
{
    public class PricedOrderItem
    {
        public ItemType ItemType { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal => UnitPrice * Quantity;
    }
}
