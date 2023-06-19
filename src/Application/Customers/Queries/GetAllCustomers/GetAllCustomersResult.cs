using Domain.Entities;

namespace Application.Customers.Queries.GetAllCustomers;

public record GetAllCustomersResult(IEnumerable<Customer> Customers);
