using System.Text.Json;
using AutoMapper;
using neighbor_chef.Models;
using neighbor_chef.Models.DTOs;
using neighbor_chef.UnitOfWork;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace neighbor_chef.Services;

public class ChefService : PersonService, IChefService
{
    protected readonly IMapper _mapper;
    
    public ChefService(IUnitOfWork unitOfWork, AccountService accountService, IMapper mapper) : base(unitOfWork, accountService)
    {
        _mapper = mapper;
    }
    
    public override async Task<Person> CreatePersonAsync(PersonRegisterDto chefDto)
    {
        if (chefDto is not ChefRegisterDto myChefDto)
        {
            throw new Exception("Invalid chef DTO.");
        }
        
        chefDto = myChefDto;
        
        var chef  = await base.CreatePersonAsync(chefDto);
        
        var jsonString = JsonConvert.SerializeObject(chef.ApplicationUser, Formatting.Indented);
        Console.WriteLine(jsonString);
        
        await _accountService.RegisterUserAsync(chef.ApplicationUser);
        await _accountService.AssignRoleAsync(chef.ApplicationUser, "Chef");

        var chefObject = new Chef
        {
            FirstName = myChefDto.FirstName,
            LastName = myChefDto.LastName,
            ApplicationUser = chef.ApplicationUser,
            ApplicationUserId = chef.ApplicationUserId,
            Address = chef.Address,
            AddressId = chef.AddressId,
            Id = chef.Id,
            DateCreated = chef.DateCreated,
            DateModified = chef.DateModified,
            ProfilePictureUrl = chef.ProfilePictureUrl,
            AdvanceNoticeDays = myChefDto.AdvanceNoticeDays,
            Description = myChefDto.Description,
            MaxOrdersPerDay = myChefDto.MaxOrdersPerDay
        };

        await _unitOfWork.GetRepository<Chef>().AddAsync(chefObject);
        await _unitOfWork.CompleteAsync();
        
        return chefObject;
    }
}