using Domain.Entities;

namespace Presentation.Contracts.Customers.GetAllCustomers
{
    public record GetAllCustomersResponse(IEnumerable<CustomerDto> Customers);
}