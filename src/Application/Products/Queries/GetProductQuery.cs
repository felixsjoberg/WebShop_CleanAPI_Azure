
using Application.Products.Response.Queries;
using MediatR;

namespace Application.Products.Queries;

public record GetProductQuery(
    Guid Id) : IRequest<GetProductResponse>;

