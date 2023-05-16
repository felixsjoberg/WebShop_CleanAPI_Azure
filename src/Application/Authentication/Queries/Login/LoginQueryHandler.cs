using Application.Authentication.Common;
using Application.Common.Errors;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using MediatR;

namespace Application.Authentication.Queries.Login;

public class LoginQueryHandler :
    IRequestHandler<LoginQuery, LoginResponse>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    private readonly IUserRepository _userRepository;


    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository )
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<LoginResponse> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        var validUser = await _userRepository.ValidateCredientals(query.Username, query.Password);
        
        if (!validUser)
        {
            throw new InvalidLoginCombination();
        }
        
        var token = _jwtTokenGenerator.GenerateToken(query.Username);

        return new LoginResponse(
            token.Token, token.Expiration);
    }
}
