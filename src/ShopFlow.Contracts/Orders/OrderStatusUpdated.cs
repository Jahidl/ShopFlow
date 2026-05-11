namespace ShopFlow.Contracts.Orders;

public record OrderStatusUpdated
{
    public Guid OrderId { get; init; }
    public string Status { get; init; } = default!;
    public string Reason { get; init; } = default!;
    public DateTime UpdatedAt { get; init; }
}
