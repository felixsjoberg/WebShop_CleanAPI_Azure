using Application.Common.Interfaces.Persistence;
using MediatR;

namespace Application.Customers.Queries.GetAllCustomers;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, GetAllCustomersResult>
{
    private readonly ICustomerRepository _customerRepository;
    public GetAllCustomersQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<GetAllCustomersResult> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.GetAllAsync();

        return new GetAllCustomersResult(customers);
    }
}