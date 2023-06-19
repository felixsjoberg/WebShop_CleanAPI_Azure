using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using MediatR;

namespace Application.Orders.Commands.DeleteOrder;
public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Unit>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IProductRepository _productRepository;

    public DeleteOrderCommandHandler(IOrderRepository orderRepository, IAddressRepository addressRepository, IProductRepository productRepository)
    {
        _productRepository = productRepository;
        _addressRepository = addressRepository;
        _orderRepository = orderRepository;
    }

    public async Task<Unit> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(command.Id) ?? throw new NotFoundException();
        var address = await _addressRepository.GetByIdAsync(order.ShippingAddressId) ?? throw new NotFoundException();

        await _orderRepository.DeleteAsync(order);
        await _addressRepository.DeleteAsync(address);

        foreach (var product in order.ProductOrders)
        {
            product.Product.IncreaseStock(product.Quantity);
            await _productRepository.UpdateAsyncWithTrack(product.Product);
        }
        return Unit.Value;
    }
}