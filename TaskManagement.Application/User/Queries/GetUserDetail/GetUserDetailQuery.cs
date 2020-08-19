using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TaskManagement.Domain.Entity;
using TaskManagement.Domain.Exceptions;

namespace TaskManagement.Application.User.Queries.GetUserDetail
{
    public class GetUserDetailQuery: IRequest<UserDetailModel>
    {
        public string UserId { get; set; }
        
        public GetUserDetailQuery(string userId)
        {
            UserId = userId;
        }
        public class Handler : IRequestHandler<GetUserDetailQuery, UserDetailModel>
        {
            private readonly UserManager<AppUser> _userManager;
            public Handler(
                UserManager<AppUser> userManager)
            {
                _userManager = userManager;
            }
            public async Task<UserDetailModel> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByIdAsync(request.UserId);
                
                if (user == null)
                    throw new NotFoundException(nameof(User), request.UserId);

                return UserDetailModel.Create(user);
            }
        }
    }
}