using Application.Orders.Queries.GetAllOrders;
using MediatR;

namespace Application.Orders.Queries.GetUserOrders;

public record GetUserOrdersQuery(Guid userId) : IRequest<GetUserOrdersResult>;