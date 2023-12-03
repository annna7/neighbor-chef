using AutoMapper;
using neighbor_chef.Models.DTOs.Authentication;

namespace neighbor_chef.Models.MappingProfile;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ChefRegisterDto, Chef>();
        CreateMap<CustomerRegisterDto, Customer>();
        CreateMap<Chef, Person>();
        CreateMap<Person, Chef>();
    }
}