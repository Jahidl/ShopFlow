namespace ShopFlow.OrderService.DTOs;

public record CreateOrderRequest
{
    public Guid CustomerId { get; init; }
    public string CustomerEmail { get; init; } = default!;
    public List<CreateOrderItemRequest> Items { get; init; } = new();
}

public record CreateOrderItemRequest
{
    public Guid ProductId { get; init; }
    public string ProductName { get; init; } = default!;
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
}