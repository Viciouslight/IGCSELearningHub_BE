using IGCSELearningHub.Domain.Common;
using IGCSELearningHub.Domain.Identity.Entities;
using IGCSELearningHub.Domain.Orders.Enums;
using IGCSELearningHub.Domain.Payments.Entities;

namespace IGCSELearningHub.Domain.Orders.Entities;

public partial class Order : BaseFullEntity
{
    public int AccountId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Account Account { get; set; } = null!;
}
