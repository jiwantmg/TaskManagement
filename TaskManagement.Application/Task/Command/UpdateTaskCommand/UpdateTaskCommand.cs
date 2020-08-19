using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entity;
using TaskManagement.Domain.Exceptions;

namespace TaskManagement.Application.Task.Command.UpdateTaskCommand
{
    public class UpdateTaskCommand: IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class Handler : IRequestHandler<UpdateTaskCommand, Unit>
    {
        private readonly ITaskManagementDbContext _context;
        public Handler(
            ITaskManagementDbContext context)
        {
            _context = context;
        }
        
        public async Task<Unit> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            // first find if any tasks exists with the id 
            var task = await _context.Set<LTask>()
                .FindAsync(request.Id);
            if(task == null)
                throw new NotFoundException("Task", request.Id);

            task.TaskName = request.Name;
            task.TaskDescription = request.Description;
            _context.Set<LTask>().Update(task);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}