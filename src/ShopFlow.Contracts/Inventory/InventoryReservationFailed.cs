namespace ShopFlow.Contracts.Inventory;

public record InventoryReservationFailed
{
    public Guid OrderId { get; init; }
    public Guid CustomerId { get; init; }
    public string CustomerEmail { get; init; } = default!;
    public string Reason { get; init; } = default!;
    public List<string> OutOfStockItems { get; init; } = new();
    public DateTime FailedAt { get; init; }
}
