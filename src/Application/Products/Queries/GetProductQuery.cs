
using Application.Products.Response.Queries;
using Domain.ValueObjects;
using MediatR;

namespace Application.Products.Queries;

public record GetProductQuery(
    ProductId Id) : IRequest<GetProductResponse>;

