using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderResult>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductOrderRepository _productOrderRepository;
    private readonly IAddressRepository _addressRepository;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IProductRepository productRepository, ICustomerRepository customerRepository, IProductOrderRepository productOrderRepository, IAddressRepository addressRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _customerRepository = customerRepository;
        _productOrderRepository = productOrderRepository;
        _addressRepository = addressRepository;
    }

    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var hasDuplicateProductIds = command.OrderItems
            .GroupBy(item => item.ProductId)
            .Any(group => group.Count() > 1);

        if (hasDuplicateProductIds)
        {
            throw new DuplicateProductsInOrderItems();
        }
        if (command.OrderItems.Count() == 0)
        {
            throw new NoProductsInOrderItems();
        }

        var address = new Address(
            command.OrderDetails.Streetaddress,
            command.OrderDetails.City,
            command.OrderDetails.Zipcode,
            command.OrderDetails.Country);

        int AddressId = await _addressRepository.AddAsync(address);
        var customer = await _customerRepository.FindByUserIdAsync(command.UserId.ToString()) ?? throw new InternalProblemWtihCreateOrder();
        customer.UpdateCustomer(command.OrderDetails.FullName);

        var order = new Order(customer.Id, customer, AddressId);
        await _orderRepository.CreateAsync(order);

        foreach (var products in command.OrderItems)
        {
            var product = await _productRepository.GetByIdAsync(products.ProductId);
            if (product is null || !product.IsActive)
            {
                await _orderRepository.DeleteAsync(order);
                throw new ProductNotFoundException();
            }
            if (product.Stock < products.Quantity)
            {
                await _orderRepository.DeleteAsync(order);
                throw new NotEnoughStockException();
            }

            product.DecreaseStock(products.Quantity);
            await _productRepository.UpdateAsync(product);

            var productId = new ProductId(products.ProductId);
            var productOrder = new ProductOrder(products.Quantity, productId, order.Id);
            await _productOrderRepository.CreateAsync(productOrder);

            productOrder.Product = product;
        }

        return new CreateOrderResult(order);
    }
}