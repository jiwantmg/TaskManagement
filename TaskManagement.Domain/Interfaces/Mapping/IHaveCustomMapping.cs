using AutoMapper;

namespace TaskManagement.Domain.Interfaces.Mapping
{
    public interface IHaveCustomMapping
    {
        void CreateCustomMappings(Profile configuration);
    }
}