using MediatR;

namespace Application.Customers.Queries.GetAllCustomers
{
    public record GetAllCustomersQuery() : IRequest<GetAllCustomersResult>;
}