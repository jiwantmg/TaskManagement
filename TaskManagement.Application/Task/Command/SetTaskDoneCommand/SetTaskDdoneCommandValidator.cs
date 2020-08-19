using FluentValidation;

namespace TaskManagement.Application.Task.Command.SetTaskDoneCommand
{
    public class SetTaskDdoneCommandValidator: AbstractValidator<SetTaskDoneCommand>
    {
        public SetTaskDdoneCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}