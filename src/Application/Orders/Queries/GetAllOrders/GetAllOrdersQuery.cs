using MediatR;

namespace Application.Orders.Queries.GetAllOrders;

public record GetAllOrdersQuery() : IRequest<GetAllOrdersResult>;