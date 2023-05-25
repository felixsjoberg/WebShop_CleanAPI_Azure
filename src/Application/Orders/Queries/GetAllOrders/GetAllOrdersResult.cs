using Domain.Entities;

namespace Application.Orders.Queries.GetAllOrders;

public record GetAllOrdersResult(IEnumerable<Order> Orders);
