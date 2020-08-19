using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entity;

namespace TaskManagement.Application.Task.Command.CreateTaskCommand
{
    public class CreateTaskCommand: IRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class Handler : IRequestHandler<CreateTaskCommand, Unit>
    {
        private readonly ITaskManagementDbContext _context;
        public Handler(
            ITaskManagementDbContext context)
        {
            _context = context;
        }
        
        public async Task<Unit> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = new LTask
            {
                TaskName = request.Name,
                TaskDescription = request.Description,
                IsDone = false
            };

            await _context.Set<LTask>().AddAsync(task, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}