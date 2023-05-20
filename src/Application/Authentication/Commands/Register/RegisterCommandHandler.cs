using Application.Authentication.Common;
using Application.Common.Errors;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using MediatR;

namespace Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<AuthenticationResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
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

        var jwttoken = await _jwtTokenGenerator.GenerateTokenAsync(command.Username);

        return new AuthenticationResult(jwttoken.Token, jwttoken.Expiration);
    }
}