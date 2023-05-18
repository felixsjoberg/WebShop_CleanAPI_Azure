using Application.Common.Errors;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using MediatR;

namespace Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponse>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {

        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;

    }

    public async Task<RegisterResponse> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {

        if (await _userRepository.ValidateCredientals(command.Username, command.Password))
        {
            throw new InvalidLoginCombination();
        }

        var jwttoken = _jwtTokenGenerator.GenerateToken(command.Username);

        var result = await _userRepository.Register(command.Email, command.Username, command.Password);

        return new RegisterResponse(jwttoken.Token, jwttoken.Expiration);
    }
}