using Application.Common.Interfaces.Persistence;
using MediatR;
namespace Application.Orders.Queries.GetAllOrders;

public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, GetAllOrdersResult>
{
    private readonly IOrderRepository _orderRepository;

    public GetAllOrdersQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<GetAllOrdersResult> Handle(GetAllOrdersQuery query, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetAllAsync();

        return new GetAllOrdersResult(orders);
    }
}