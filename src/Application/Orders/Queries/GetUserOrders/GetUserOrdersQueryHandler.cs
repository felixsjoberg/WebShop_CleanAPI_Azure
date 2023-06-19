using Application.Common.Interfaces.Persistence;
using Application.Orders.Queries.GetUserOrders;
using MediatR;
namespace Application.Orders.Queries.GetAllOrders;

public class GetUserOrdersQueryHandler : IRequestHandler<GetUserOrdersQuery, GetUserOrdersResult>
{
    private readonly IOrderRepository _orderRepository;

    public GetUserOrdersQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<GetUserOrdersResult> Handle(GetUserOrdersQuery query, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetUserOrdersAsync(query.userId);

        return new GetUserOrdersResult(orders);
    }
}