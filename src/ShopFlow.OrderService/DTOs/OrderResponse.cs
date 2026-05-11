namespace ShopFlow.OrderService.DTOs;

public record OrderResponse
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; init; }
    public string CustomerEmail { get; init; } = default!;
    public string Status { get; init; } = default!;
    public string StatusReason { get; init; } = default!;
    public decimal TotalAmount { get; init; }
    public DateTime PlacedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public List<OrderLineItemResponse> LineItems { get; init; } = new();
}

public record OrderLineItemResponse
{
    public Guid ProductId { get; init; }
    public string ProductName { get; init; } = default!;
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
}