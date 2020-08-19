using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Dtos;
using TaskManagement.Domain.Entity;
using TaskManagement.Domain.Exceptions;

namespace TaskManagement.Application.User.Command.LoginCommand
{
    public class LoginCommand: IRequest<AuthDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class Handler : IRequestHandler<LoginCommand, AuthDto>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtProvider _jwtProvider;
        public Handler(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IJwtProvider jwtProvider
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtProvider = jwtProvider;
        }
        
        public async Task<AuthDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            // var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, request.Password);
            // Console.Write($"User Detail {user.Email} Password Result {isPasswordCorrect}");
            if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                var identity = new ClaimsIdentity();
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.Email));
                
                // user logged in
                string role = user.IsAdmin ? "Admin" : "User";
                return _jwtProvider.Create(user, role);
            }
            Console.Write(user);
            throw new InvalidCredentialsException(request.Email);
        }
    }
}