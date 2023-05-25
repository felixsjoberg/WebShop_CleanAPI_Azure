using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, CreateOrderResult>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductOrderRepository _productOrderRepository;

    public CreateOrderHandler(IOrderRepository orderRepository, IProductRepository productRepository, ICustomerRepository customerRepository, IProductOrderRepository productOrderRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _customerRepository = customerRepository;
        _productOrderRepository = productOrderRepository;
    }

    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        int customerId = 0;
        var customer = new Customer(
         command.Customer.FullName,
         command.Customer.Streetaddress,
         command.Customer.City,
         command.Customer.Zipcode,
         command.Customer.Country,
         command.Customer.CountryCode,
         command.UserId.ToString());

        var hasDuplicateProductIds = command.OrderItems
            .GroupBy(item => item.ProductId)
            .Any(group => group.Count() > 1);

        if (hasDuplicateProductIds)
        {
            throw new DuplicateProductsInOrderItems();
        }

        // Check if any customer already has the user that makes the order, if so update the customer, otherwise create a new customer.
        var customers = await _customerRepository.GetAllAsync();
        if (customers.Any(c => c.UserId == command.UserId.ToString()))
        {
            var customer2 = customers.FirstOrDefault(c => c.UserId == command.UserId.ToString());
            customer.Id = customer2.Id;
            customerId = customer.Id;
            await _customerRepository.UpdateAsync(customer);
        }
        else
        {
            customerId = await _customerRepository.AddAsync(customer);
        }

        var order = new Order(customerId);
        await _orderRepository.CreateAsync(order);
        order.Customer = customer;
        foreach (var products in command.OrderItems)
        {
            var product = await _productRepository.GetByIdAsync(products.ProductId);
            if (product is null)
            {
                await _orderRepository.DeleteAsync(order);
                throw new ProductNotFoundException();
            }
            if (product.Stock < products.Quantity)
            {
                throw new NotEnoughStockException();
            }

            product.DecreaseStock(products.Quantity);
            await _productRepository.UpdateAsync(product);

            var productId = new ProductId(products.ProductId);
            var productOrder = new ProductOrder(products.Quantity, productId, order.Id);
            await _productOrderRepository.CreateAsync(productOrder);

            // Fetch the corresponding Product entity
            productOrder.Product = product;
        }

        return new CreateOrderResult(order);
    }
}