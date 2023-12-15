// using System.Text.Json;
using AutoMapper;
using neighbor_chef.Exceptions.Dates;
using neighbor_chef.Exceptions.People;
using neighbor_chef.Models;
using neighbor_chef.Models.DTOs;
using neighbor_chef.Models.DTOs.Authentication;
using neighbor_chef.UnitOfWork;
using Newtonsoft.Json;

// using Newtonsoft.Json;
// using JsonSerializer = Newtonsoft.Json.JsonSerializer;

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
        
        // var jsonString = JsonConvert.SerializeObject(chef.ApplicationUser, Formatting.Indented);
        // Console.WriteLine(jsonString);
        
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
    
    private static bool IsDateAvailable(Chef chef, DateTime date)
    {
        // TODO: Check if orders are already placed for this date.
        return date >= DateTime.Now.AddDays(chef.AdvanceNoticeDays);
    }
    
    public async Task<string> AddAvailableDateAsync(Guid chefId, DateDto date)
    {
        var chef = await _unitOfWork.GetRepository<Chef>().GetByIdAsync(chefId);
        if (chef == null)
        {
            throw new ChefNotFoundException();
        }

        var availableDates = chef.AvailableDates;
        
        DateTime parsedDate = new DateTime(date.Year, date.Month, date.Day);
        if (availableDates.Contains(parsedDate))
        {
            throw new DateAlreadyAvailableException();
        }
        
        Console.WriteLine("Adding date" + parsedDate);
        if (!IsDateAvailable(chef, parsedDate))
        {
            throw new DateCantBeAvailableException();
        }
        
        availableDates.Add(parsedDate);
        chef.AvailableDates = availableDates; // this will automatically update AvailableDatesJson

        await _unitOfWork.GetRepository<Chef>().UpdateAsync(chef);
        await _unitOfWork.CompleteAsync();
        return JsonConvert.SerializeObject(chef.AvailableDates);
    }
    
    public async Task<List<DateTime>> GetAvailableDatesAsync(Guid chefId)
    {
        var chef = await _unitOfWork.GetRepository<Chef>().GetByIdAsync(chefId);
        if (chef == null)
        {
            throw new ChefNotFoundException();
        }

        chef.AvailableDates = chef.AvailableDates.Where(date => IsDateAvailable(chef, date)).ToList();
       
        await _unitOfWork.GetRepository<Chef>().UpdateAsync(chef);
        await _unitOfWork.CompleteAsync();
         
        return chef.AvailableDates;
    }

    public async Task RemoveAvailableDateAsync(Guid chefId, DateDto date)
    {
        var chef = await _unitOfWork.GetRepository<Chef>().GetByIdAsync(chefId);
        if (chef == null)
        {
            throw new ChefNotFoundException();
        }
        
        DateTime parsedDate = new DateTime(date.Year, date.Month, date.Day);
        if (!chef.AvailableDates.Contains(parsedDate))
        {
            throw new DateNotFoundException();
        }
        chef.AvailableDates.Remove(parsedDate);
        await _unitOfWork.GetRepository<Chef>().UpdateAsync(chef);
        await _unitOfWork.CompleteAsync();
    }
}