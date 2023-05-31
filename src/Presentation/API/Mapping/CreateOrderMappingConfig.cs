using Application.Orders.Commands.CreateOrder;
using Application.Orders.Common.DTOs;
using Mapster;
using Presentation.Contracts;

namespace Presentation.API.Mapping;
public class CreateOrderMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateOrderData, CreateOrderCommand>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.OrderItems, src => src.OrderItems)
            .Map(dest => dest.OrderDetails, src => src.Customer);

        config.NewConfig<CreateOrderResult, OrderDto>()
            .Map(dest => dest.Id, src => src.Order.Id.Value)
            .Map(dest => dest.Status, src => src.Order.Status)
            .Map(dest => dest.OrderDate, src => src.Order.OrderDate)
            .Map(dest => dest.Customer, src => src.Order.Customer)
            .Map(dest => dest.Customer.FullName, src => src.Order.Customer.FullName)
            .Map(dest => dest.Customer.Streetaddress, src => src.Order.ShippingAddress.Streetaddress)
            .Map(dest => dest.Customer.City, src => src.Order.ShippingAddress.City)
            .Map(dest => dest.Customer.Zipcode, src => src.Order.ShippingAddress.Zipcode)
            .Map(dest => dest.Customer.Country, src => src.Order.ShippingAddress.Country)
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