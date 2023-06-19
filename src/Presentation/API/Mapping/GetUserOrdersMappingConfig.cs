using Application.Orders.Common.DTOs;
using Domain.Entities;
using Mapster;

namespace Presentation.API.Mapping;
public class GetUserOrdersMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Order, OrderDto>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Customer, src => src.Customer)
            .Map(dest => dest.Customer.FullName, src => src.Customer.FullName)
            .Map(dest => dest.Customer.Streetaddress, src => src.ShippingAddress.Streetaddress)
            .Map(dest => dest.Customer.City, src => src.ShippingAddress.City)
            .Map(dest => dest.Customer.Zipcode, src => src.ShippingAddress.Zipcode)
            .Map(dest => dest.Customer.Country, src => src.ShippingAddress.Country)
            .Map(dest => dest.Products, src => src.ProductOrders
            .Select(po => new ProductsOnOrderDto
        (
            po.Product.Id.Value,
            po.Product.Name,
            po.Product.Price,
            po.Quantity
        )).ToArray());
    }
}