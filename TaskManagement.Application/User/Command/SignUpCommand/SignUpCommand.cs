using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TaskManagement.Domain.Entity;

namespace TaskManagement.Application.User.Command.SignUpCommand
{
    public class SignUpCommand: IRequest
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
        public string Organization { get; set; }
    }

    public class Handler : IRequestHandler<SignUpCommand, Unit>
    {
        private readonly UserManager<AppUser> _userManager;
        
        public Handler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        
        public async Task<Unit> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var user = new AppUser
            {
                UserName = request.UserName,
                Email = request.Email,
                Organization = request.Organization,
                FirstName = request.FirstName,
                LastName = request.LastName,
                IsAdmin = request.IsAdmin
            };

            await _userManager.CreateAsync(user, request.Password);
            return Unit.Value;
        }
    }
}