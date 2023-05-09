// using Application.Authentication.Common;
// using Application.Common.Interfaces.Authentication;
// using BankApplication.Application.Common.Interfaces.Persistence;
// using MediatR;

// namespace CA.Application.Authentication.Queries.Login;

// public class LoginQueryHandler :
//     IRequestHandler<LoginQuery, AuthenticationResponse>
// {
//     private readonly IJwtTokenGenerator _jwtTokenGenerator;

//     private readonly IUserRepository _userRepository;


//     public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository )
//     {
//         _jwtTokenGenerator = jwtTokenGenerator;
//         _userRepository = userRepository;
//     }

//     public async Task<AuthenticationResponse> Handle(LoginQuery query, CancellationToken cancellationToken)
//     {
//         var user = await _userRepository.GetByEmail(query.Email);

//         if (user is null)
//         {
//             throw new Exception("User does not exist");
//         }

//         if (user.Password != query.Password)
//         {
//             throw new Exception("Invalid password");
//         }

//         var token = _jwtTokenGenerator.GenerateToken(user);

//         return new AuthenticationResponse(
//             user.UserId,
//             token);
//     }
// }
