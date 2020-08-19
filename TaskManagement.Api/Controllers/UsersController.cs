using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.User;
using TaskManagement.Application.User.Command.LoginCommand;
using TaskManagement.Application.User.Command.SignUpCommand;
using TaskManagement.Application.User.Queries.GetAllUsers;
using TaskManagement.Application.User.Queries.GetUserDetail;
using TaskManagement.Domain.Dtos;

namespace TaskManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController: ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<UserListViewModel> Get(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetUserssListQuery(), cancellationToken);
        }
        
        [HttpGet("{userId}")]
        public async Task<UserDetailModel> GetUserById(string userId, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetUserDetailQuery(userId), cancellationToken);
        }
        
        [HttpPost]
        public async Task<Unit> CreateUser(SignUpCommand signUpCommand, CancellationToken cancellationToken)
        {
            // check if organization is selected or not
            signUpCommand.IsAdmin = false;
            return await _mediator.Send(signUpCommand, cancellationToken);
        }

        [HttpPost("admin")]
        public async Task<Unit> CreateAdmin(SignUpCommand signUpCommand, CancellationToken cancellationToken)
        {
            signUpCommand.IsAdmin = true;
            return await _mediator.Send(signUpCommand, cancellationToken);
        }

        [HttpPost("authenticate")]
        public async Task<AuthDto> AuthenticateUser(LoginCommand loginCommand, CancellationToken cancellationToken)
        {
            return await _mediator.Send(loginCommand, cancellationToken);
        }
    }
}