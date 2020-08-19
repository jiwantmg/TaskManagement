using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entity;

namespace TaskManagement.Application.User.Queries.GetAllUsers
{
    public class GetUserssListQuery: IRequest<UserListViewModel>
    {
        public class Handler: IRequestHandler<GetUserssListQuery, UserListViewModel>
        {
            private readonly ITaskManagementDbContext _context;
            private IMapper _mapper;
            public Handler(
                ITaskManagementDbContext context,
                IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<UserListViewModel> Handle(GetUserssListQuery request, CancellationToken cancellationToken)
            {
                return new UserListViewModel
                {
                    Users = await _context
                        .Set<AppUser>()
                        .Where(u => !u.IsAdmin)
                        .ProjectTo<UserLookupModel>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken)
                };
            }
        }
    }
}