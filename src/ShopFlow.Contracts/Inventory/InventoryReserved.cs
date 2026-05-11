namespace ShopFlow.Contracts.Inventory;

public record InventoryReserved
{
    public Guid OrderId { get; init; }
    public Guid CustomerId { get; init; }
    public string CustomerEmail { get; init; } = default!;
    public List<ReservedItem> ReservedItems { get; init; } = new();
    public DateTime ReservedAt { get; init; }
}

public record ReservedItem
{
    public Guid ProductId { get; init; }
    public string ProductName { get; init; } = default!;
    public int Quantity { get; init; }
}
