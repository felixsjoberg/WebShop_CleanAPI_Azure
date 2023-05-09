// using Application.Authentication.Common;
// using Application.Common.Interfaces.Authentication;
// using Domain.Entities;
// using MediatR;
// using BankApplication.Application.Common.Interfaces.Persistence;

// namespace CA.Application.Authentication.Commands.Register;

// public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResponse>
// {
//     private readonly IJwtTokenGenerator _jwtTokenGenerator;

//     private readonly IUserRepository _userRepository;

//     public RegisterCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
//     {

//         _jwtTokenGenerator = jwtTokenGenerator;
//         _userRepository = userRepository;

//     }

//     public async Task<AuthenticationResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
//     {
//         var user = new User
//         {
//             Email = request.Email,
//             Password = request.Password
//         };
//         if (await _userQueryRepository.GetByEmail(user.Email) is not null)
//         {
//             throw new Exception("User already exists");
//         }

//         var newUser = await _userCommandRepository.AddAsync(user);

//         var token = _jwtTokenGenerator.GenerateToken(user);

//         return new AuthenticationResponse
//         (newUser.UserId,
//         token);
//     }
// }