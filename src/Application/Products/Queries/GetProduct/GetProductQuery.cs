
using Application.Products.Queries.GetProduct;
using MediatR;

namespace Application.Products.Queries;

public record GetProductQuery(
    int Id) : IRequest<GetProductResult>;