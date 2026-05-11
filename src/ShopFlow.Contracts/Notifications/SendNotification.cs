namespace ShopFlow.Contracts.Notifications;

public record SendNotification
{
    public Guid OrderId { get; init; }
    public string RecipientEmail { get; init; } = default!;
    public string Subject { get; init; } = default!;
    public string Body { get; init; } = default!;
    public NotificationType Type { get; init; }
    public DateTime CreatedAt { get; init; }
}

public enum NotificationType
{
    OrderConfirmation,
    InventoryReserved,
    InventoryFailed,
    OrderCompleted
}
