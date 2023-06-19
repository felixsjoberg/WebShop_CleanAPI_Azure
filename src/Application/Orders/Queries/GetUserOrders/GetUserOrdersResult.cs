using Domain.Entities;

public record GetUserOrdersResult(IEnumerable<Order> Orders);