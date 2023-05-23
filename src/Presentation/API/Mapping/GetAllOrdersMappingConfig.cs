using Application.Orders.Common.DTOs;
using Domain.Entities;
using Mapster;

namespace Presentation.API.Mapping;
public class GetAllOrdersMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Order, OrderDto>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Customer, src => src.Customer)
            .Map(dest => dest.Customer.Name, src => src.Customer.FullName)
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
