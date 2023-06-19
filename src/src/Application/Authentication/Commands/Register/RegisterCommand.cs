using MediatR;

namespace Application.Authentication.Commands.Register;

public record RegisterCommand(
    string Username,
    string Email,
    string Password,
    RegisterCustomerCommand Customer
    ): IRequest<RegsiterResult>;

    public record RegisterCustomerCommand(
    string FullName,
    string Streetaddress,
    string City,
    string Zipcode,
    string Country,
    string CountryCode
);