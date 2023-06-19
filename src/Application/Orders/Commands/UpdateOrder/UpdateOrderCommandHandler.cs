using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using MediatR;

namespace Application.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Unit>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductOrderRepository _productOrderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IAddressRepository _addressRepository;

    public UpdateOrderCommandHandler(IOrderRepository orderRepository, ICustomerRepository customerRepository, IProductRepository productRepository, IProductOrderRepository productOrderRepository, IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
        _productOrderRepository = productOrderRepository;
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _productRepository = productRepository;
    }

    public async Task<Unit> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(command.Customer.Id) ?? throw new NotFoundCustomer();
        var order = await _orderRepository.GetByIdAsync(command.Id) ?? throw new NotFoundOrder();
        if (order.Status == OrderStatus.Shipped || order.Status == OrderStatus.Delivered)
        {
            throw new OrderAlreadyShipped();
        }
        if (!command.OrderItems.Any())
        {
            throw new NoProductsInOrderItems();
        }

        // Find all products that has been removed from the order
        var productIdsToRemove = order.ProductOrders
        .Where(productOrder => !command.OrderItems.Any(item => item.ProductId == productOrder.ProductId.Value))
        .Select(productOrder => productOrder.ProductId.Value)
        .ToList();

        foreach (var productIdToRemove in productIdsToRemove)
        {
            var product = await _productRepository.GetByIdAsync(productIdToRemove) ?? throw new ProductNotFoundException();
            product.IncreaseStock(order.ProductOrders.FirstOrDefault(productOrder => productOrder.ProductId.Value == productIdToRemove)!.Quantity);
            await _productRepository.UpdateAsyncWithTrack(product);
        }

        List<ProductOrder> productOrders = new();
        order.Status = command.Status switch
        {
            0 => OrderStatus.Pending,
            1 => OrderStatus.Shipped,
            2 => OrderStatus.Delivered,
            _ => throw new NotImplementedException()
        };

        var updatedAddress = new Address(
            order.ShippingAddressId,
            command.Customer.Streetaddress,
            command.Customer.City,
            command.Customer.Zipcode,
            command.Customer.Country);

        order.ShippingAddress = updatedAddress;
        foreach (var products in command.OrderItems)
        {
            var product = await _productRepository.GetByIdAsync(products.ProductId) ?? throw new ProductNotFoundException();

            var productid = new ProductId(products.ProductId);
            var productOrder = new ProductOrder(products.Quantity, productid, order.Id);

            var oldProductQuantity = order.ProductOrders.FirstOrDefault(x => x.ProductId.Equals(productid))?.Quantity;
            if (oldProductQuantity == null)
            {
                product.DecreaseStock(products.Quantity);
                await _productRepository.UpdateAsync(product);
            }

            if (product.Stock < (products.Quantity - oldProductQuantity ?? 0))
            {
                throw new NotEnoughStockException();
            }
            if (!product.IsActive)
            {
                throw new ProductNotActiveException();
            }

            if (products.Quantity > oldProductQuantity)
            {
                var difference = products.Quantity - (oldProductQuantity ?? 0);
                product.DecreaseStock((int)difference);
                await _productRepository.UpdateAsyncWithTrack(product);
            }
            else if (products.Quantity < oldProductQuantity)
            {
                var difference = (oldProductQuantity ?? 0) - products.Quantity;
                product.IncreaseStock((int)difference);
                await _productRepository.UpdateAsyncWithTrack(product);
            }

            if (order.ProductOrders.Any(x => x.ProductId.Value.Equals(products.ProductId)))
            {
                var productOrderToUpdate = order.ProductOrders.FirstOrDefault(x => x.ProductId.Equals(productid));
                productOrderToUpdate!.UpdateQuantity(products.Quantity);
                await _productOrderRepository.UpdateAsync(productOrderToUpdate!);
            }
            else
            {
                await _productOrderRepository.CreateAsync(productOrder);
            }
            // Fetch the corresponding Product entity
            productOrders.Add(productOrder);
        }
        order.ProductOrders.Clear();
        order.ProductOrders = productOrders;
        customer.UpdateCustomer(command.Customer.FullName);
        await _addressRepository.UpdateAsync(updatedAddress);
        await _orderRepository.UpdateAsync(order);
        await _customerRepository.UpdateAsync(customer!);

        return Unit.Value;
    }
}
