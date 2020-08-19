using System.Collections;
using System.Collections.Generic;

namespace TaskManagement.Application.Task
{
    public class TaskListViewModel
    {
        public IList<TaskLookupModel> Tasks { get; set; }
    }
}