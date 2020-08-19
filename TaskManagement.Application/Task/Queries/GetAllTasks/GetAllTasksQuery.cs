using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entity;

namespace TaskManagement.Application.Task.Queries.GetAllTasks
{
    public class GetAllTasksQuery: IRequest<TaskListViewModel>
    {
    }

    public class Handler : IRequestHandler<GetAllTasksQuery, TaskListViewModel>
    {
        private readonly ITaskManagementDbContext _context;
        private readonly IMapper _mapper;
        public Handler(
            ITaskManagementDbContext context,
            IMapper mapper
            )
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TaskListViewModel> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            return new TaskListViewModel
            {
                Tasks = await _context.Set<LTask>()
                    .ProjectTo<TaskLookupModel>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}