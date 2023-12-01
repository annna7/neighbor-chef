using AutoMapper;
using neighbor_chef.Models.DTOs;

namespace neighbor_chef.Models.MappingProfile;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ChefRegisterDto, Chef>();
        CreateMap<CustomerRegisterDto, Customer>();
    }
}