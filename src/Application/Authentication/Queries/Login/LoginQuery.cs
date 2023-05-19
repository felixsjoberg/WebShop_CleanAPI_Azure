using Application.Authentication.Common;
using MediatR;

namespace Application.Authentication.Queries.Login;
public record LoginQuery(
    string Username,
    string Password) : IRequest<AuthenticationResult>;

