// using System;
// using MediatR;
// using FinTasker.Application.Features.Auth.Commands;
// using FinTasker.Domain.Entities;
// using FinTasker.Domain.Interfaces;
// using BCrypt.Net;

// namespace FinTasker.Application.Features.Auth.Commands.Register
// {

//     public class RegisterHandler
//     {
//         public class RegisterHandler : IRequestHandler<RegisterCommand, string>
//         {
//             private readonly IUserRepository _userRepository;

//             public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
//             {
//                 var existing = await _userRepository.GetByEmailAsync(request.Email);

//                 if (existing != null)
//                     throw new Exception("Email already exists");

//                 var user = new User
//                 {
//                     Id = Guid.NewGuid(),
//                     Email = request.Email,
//                     PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
//                     Role = request.Role
//                 };

//                 await _userRepository.AddAsync(user);

//                 return "Register success";
//             }
//         }


//     }
// }

