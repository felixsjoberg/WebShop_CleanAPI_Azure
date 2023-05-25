using Application.Customers.CreateOrUpdateCustomer;
using Application.Orders.Commands.CreateOrder;
using Application.Orders.Common.DTOs;
using Domain.Entities;
using Mapster;
using Presentation.Contracts;
using Presentation.Contracts.Orders.CreateOrder;

namespace Presentation.API.Mapping;
public class CreateOrderMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateOrderData, CreateOrderCommand>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.OrderItems, src => src.OrderItems)
            .Map(dest => dest.Customer, src => src.Customer);

        config.NewConfig<CreateOrderData, CreateOrUpdateCustomerCommand>()
        .Map(dest => dest.FullName, src => src.Customer.FullName)
        .Map(dest => dest.Streetaddress, src => src.Customer.Streetaddress)
        .Map(dest => dest.City, src => src.Customer.City)
        .Map(dest => dest.Zipcode, src => src.Customer.Zipcode)
        .Map(dest => dest.Country, src => src.Customer.Country)
        .Map(dest => dest.CountryCode, src => src.Customer.CountryCode)
        .Map(dest => dest.UserId, src => src.UserId);

        config.NewConfig<CreateOrderResult, OrderDto>()
            .Map(dest => dest.Id, src => src.Order.Id.Value)
            .Map(dest => dest.Status, src => src.Order.Status)
            .Map(dest => dest.OrderDate, src => src.Order.OrderDate)
            .Map(dest => dest.Customer, src => src.Order.Customer)
            .Map(dest => dest.Products, src => src.Order.ProductOrders
            .Select(po => new ProductsOnOrderDto
        (
            po.Product.Id.Value,
            po.Product.Name,
            po.Product.Price,
            po.Quantity
        )).ToArray());
    }
}