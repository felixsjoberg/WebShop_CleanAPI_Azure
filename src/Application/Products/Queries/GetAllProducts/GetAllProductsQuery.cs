using Domain.Entities;
using MediatR;

namespace Application.Products.Response.Queries;

public record GetAllProductsQuery() : IRequest<GetAllProductsResult>
{
}