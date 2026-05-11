namespace ShopFlow.OrderService.Models;

public class Order
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string CustomerEmail { get; set; } = default!;
    public OrderStatus Status { get; set; }
    public string StatusReason { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public DateTime PlacedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public List<OrderLineItem> LineItems { get; set; } = new();
}

public enum OrderStatus
{
    Placed,
    InventoryReserved,
    InventoryFailed,
    Completed,
    Cancelled
}