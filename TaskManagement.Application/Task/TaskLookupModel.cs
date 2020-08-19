using AutoMapper;
using TaskManagement.Domain.Entity;
using TaskManagement.Domain.Interfaces.Mapping;

namespace TaskManagement.Application.Task
{
    public class TaskLookupModel: IHaveCustomMapping
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
        public void CreateCustomMappings(Profile configuration)
        {
            configuration.CreateMap<LTask, TaskLookupModel>()
                .ForMember(dest => dest.Id, src => src.MapFrom(t => t.TaskId))
                .ForMember(dest => dest.Name, src => src.MapFrom(t => t.TaskName))
                .ForMember(dest => dest.Description, src => src.MapFrom(t => t.TaskDescription))
                .ForMember(dest => dest.IsDone, src => src.MapFrom(t => t.IsDone));
        }
    }
}