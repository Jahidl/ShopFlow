using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopFlow.OrderService.Data;
using ShopFlow.OrderService.DTOs;
using ShopFlow.OrderService.Models;

namespace ShopFlow.OrderService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly OrderDbContext _db;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(OrderDbContext db, ILogger<OrdersController> logger)
    {
        _db = db;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = request.CustomerId,
            CustomerEmail = request.CustomerEmail,
            Status = OrderStatus.Placed,
            PlacedAt = DateTime.UtcNow,
            TotalAmount = request.Items.Sum(i => i.Quantity * i.UnitPrice),
            LineItems = request.Items.Select(i => new OrderLineItem
            {
                Id = Guid.NewGuid(),
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList()
        };

        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        _logger.LogInformation("Order {OrderId} created for customer {CustomerId}", 
            order.Id, order.CustomerId);

        return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, MapToResponse(order));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrder(Guid id)
    {
        var order = await _db.Orders
            .Include(o => o.LineItems)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order is null)
            return NotFound();

        return Ok(MapToResponse(order));
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _db.Orders
            .Include(o => o.LineItems)
            .OrderByDescending(o => o.PlacedAt)
            .ToListAsync();

        return Ok(orders.Select(MapToResponse));
    }

    private static OrderResponse MapToResponse(Order order) => new()
    {
        Id = order.Id,
        CustomerId = order.CustomerId,
        CustomerEmail = order.CustomerEmail,
        Status = order.Status.ToString(),
        StatusReason = order.StatusReason,
        TotalAmount = order.TotalAmount,
        PlacedAt = order.PlacedAt,
        UpdatedAt = order.UpdatedAt,
        LineItems = order.LineItems.Select(li => new OrderLineItemResponse
        {
            ProductId = li.ProductId,
            ProductName = li.ProductName,
            Quantity = li.Quantity,
            UnitPrice = li.UnitPrice
        }).ToList()
    };
}