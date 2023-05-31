using MediatR;

namespace Application.Orders.Commands.DeleteOrder;

public record DeleteOrderCommand(Guid Id): IRequest<Unit>;
