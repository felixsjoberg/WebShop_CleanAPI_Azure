using MediatR;

namespace Application.Authentication.Commands.Register;

public record RegisterCommand(
    string Username,
    string Email,
    string Password): IRequest<RegisterResponse>;