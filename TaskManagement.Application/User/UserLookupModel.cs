using AutoMapper;
using TaskManagement.Domain.Entity;
using TaskManagement.Domain.Interfaces.Mapping;

namespace TaskManagement.Application.User
{
    public class UserLookupModel: IHaveCustomMapping
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public void CreateCustomMappings(Profile configuration)
        {
            configuration.CreateMap<AppUser, UserLookupModel>()
                .ForMember(dest => dest.FirstName, src => src.MapFrom(u => u.FirstName))
                .ForMember(dest => dest.LastName, src => src.MapFrom(u => u.LastName));
        }
    }
}