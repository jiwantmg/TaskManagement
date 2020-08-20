using System;
using System.Threading;
using System.Threading.Tasks;
using Finbuckle.MultiTenant;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Task;
using TaskManagement.Application.Task.Command.CreateTaskCommand;
using TaskManagement.Application.Task.Command.SetTaskDoneCommand;
using TaskManagement.Application.Task.Command.UpdateTaskCommand;
using TaskManagement.Application.Task.Queries.GetAllTasks;

namespace TaskManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController: ControllerBase
    {
        private readonly IMediator _mediator;
        public TasksController(
            IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<Unit> CreateTasks(CreateTaskCommand createTaskCommand, CancellationToken cancellationToken)
        {
            return await _mediator.Send(createTaskCommand, cancellationToken);
        }

        [HttpGet]
        public async Task<TaskListViewModel> Get(CancellationToken cancellationToken)
        {
            Console.WriteLine(HttpContext.GetMultiTenantContext()?.TenantInfo.ConnectionString);
            return await _mediator.Send(new GetAllTasksQuery(), cancellationToken);
        }

        [HttpPut]
        public async Task<Unit> Update(UpdateTaskCommand updateTaskCommand, CancellationToken cancellationToken)
        {
            return await _mediator.Send(updateTaskCommand, cancellationToken);
        }

        [HttpPut("{taskId}/SetDone")]
        public async Task<Unit> SetTaskDone(int taskId, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new SetTaskDoneCommand(taskId), cancellationToken);
        }
    }
}