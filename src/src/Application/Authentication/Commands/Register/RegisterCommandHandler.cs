using Application.Common.Errors;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegsiterResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly ICustomerRepository _customerRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAddressRepository _addressRepository;

    public RegisterCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator, IAddressRepository addressRepository, ICustomerRepository customerRepository)
    {
        _addressRepository = addressRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _customerRepository = customerRepository;
        _userRepository = userRepository;
    }

    public async Task<RegsiterResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {

        if (await _userRepository.ValidateUsername(command.Username))
        {
            throw new UserExistsException();
        }
        if (await _userRepository.ValidateEmail(command.Email))
        {
            throw new EmailExistsException();
        }

        var result = await _userRepository.Register(command.Email, command.Username, command.Password);

        var address = new Address(command.Customer.Streetaddress, command.Customer.City, command.Customer.Zipcode, command.Customer.Country);
        var addressId = await _addressRepository.AddAsync(address);

        var customer = new Customer(command.Customer.FullName, result, addressId);
        var customerId = await _customerRepository.AddAsync(customer);

        var jwttoken = await _jwtTokenGenerator.GenerateTokenAsync(command.Username);

        return new RegsiterResult(jwttoken.Token, jwttoken.Expiration, customerId, addressId);
    }
}