namespace Presentation.Contracts.Orders.GetOrders
{
    public record GetOrdersResponse(IEnumerable<OrderResponse> Orders);

    public record OrderResponse(
        int Id,
        string CustomerName,
        string CustomerEmail,
        string CustomerPhone,
        string CustomerAddress,
        string CustomerCity,
        string CustomerState,
        string CustomerZipCode,
        decimal Total,
        IEnumerable<OrderItemResponse> Items);

    public record OrderItemResponse(
        int Id,
        string Name,
        decimal Price,
        int Quantity);
}