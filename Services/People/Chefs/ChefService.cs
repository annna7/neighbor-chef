// using System.Text.Json;

using System.Globalization;
using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using neighbor_chef.Exceptions.Dates;
using neighbor_chef.Exceptions.People;
using neighbor_chef.Models;
using neighbor_chef.Models.DTOs;
using neighbor_chef.Models.DTOs.Authentication;
using neighbor_chef.Specifications;
using neighbor_chef.UnitOfWork;
using Newtonsoft.Json;

// using Newtonsoft.Json;
// using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace neighbor_chef.Services.People.Chefs;

public class ChefService : PersonService, IChefService
{
    public ChefService(IUnitOfWork unitOfWork, IAccountService accountService) : base(unitOfWork, accountService)
    {
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

    public async Task<List<Order>> GetOrdersForChef(Guid chefId, DateTime? dateTime = null)
    {
        var chef = await _unitOfWork.GetRepository<Chef>().GetByIdNoTrackingAsync(chefId);
        if (chef == null)
        {
            throw new ChefNotFoundException();
        }
        if (dateTime.HasValue)
        {
            var orders = await _unitOfWork.GetRepository<Order>().FindWithSpecificationPatternAsync(
                new OrdersForChefAndDate(chefId, dateTime.Value));
            return orders.ToList();
        } else {
            var orders = await _unitOfWork.GetRepository<Order>().FindWithSpecificationPatternAsync(
                new OrdersForChefAndDate(chefId));
            return orders.ToList();
        }
    }
    
    public async Task<Chef?> GetChefAsync(string email, bool asNoTracking = false)
    {
        var chefRepository = _unitOfWork.GetRepository<Chef>();
        var fullChefByEmailSpecification = new FullChefWithEmailSpecification(email);
        return await chefRepository.FindFirstOrDefaultWithSpecificationPatternAsync(fullChefByEmailSpecification, asNoTracking);
    }
    
    public async Task<Chef?> GetChefAsync(Guid id, bool asNoTracking = false)
    {
        var chefRepository = _unitOfWork.GetRepository<Chef>();
        var fullChefByIdSpecification = new FullChefWithIdSpecification(id);
        return await chefRepository.FindFirstOrDefaultWithSpecificationPatternAsync(fullChefByIdSpecification, asNoTracking);
    }

    public async Task<bool> IsDateAvailable(Chef chef, DateTime date)
    {
        // TODO: Check if orders are already placed for this date.
        if (date < DateTime.Now.AddDays(chef.AdvanceNoticeDays))
        {
            return false;
        }
        var ordersOnDate = await GetOrdersForChef(chef.Id, date);
        return ordersOnDate.Count < chef.MaxOrdersPerDay;
    }
    
    public async Task<string> AddAvailableDateAsync(Guid chefId, string date)
    {
        var chef = await _unitOfWork.GetRepository<Chef>().GetByIdNoTrackingAsync(chefId);
        if (chef == null)
        {
            throw new ChefNotFoundException();
        }
        
        var parsedDate = ConvertStringToDateTime(date);
        var availableDates = chef.AvailableDates;
        if (availableDates.Contains(parsedDate))
        {
            throw new DateAlreadyAvailableException();
        }
        
        Console.WriteLine("Adding date" + parsedDate);
        if (!(await IsDateAvailable(chef, parsedDate)))
        {
            throw new DateCantBeAvailableException();
        }
        
        availableDates.Add(parsedDate);
        chef.AvailableDates = availableDates; 
        
        await _unitOfWork.GetRepository<Chef>().UpdateAsync(chef);
        await _unitOfWork.CompleteAsync();
        
        Console.WriteLine("updated dates hopefully", chef.AvailableDates, chef.AvailableDatesJson);
        return JsonConvert.SerializeObject(chef.AvailableDates);
    }
    
    public async Task<List<DateTime>> GetAvailableDatesAsync(Guid chefId)
    {
        var chef = await _unitOfWork.GetRepository<Chef>().GetByIdNoTrackingAsync(chefId);
        if (chef == null)
        {
            throw new ChefNotFoundException();
        }
        
        var availableDates = new List<DateTime>();
        foreach (var date in chef.AvailableDates)
        {
            if (await IsDateAvailable(chef, date))
            {
                availableDates.Add(date);
            }
        }
        chef.AvailableDates = availableDates;
        
        await _unitOfWork.GetRepository<Chef>().UpdateAsync(chef);
        await _unitOfWork.CompleteAsync();
         
        return chef.AvailableDates;
    }

    public async Task RemoveAvailableDateAsync(Guid chefId, string date)
    {
        var chef = await _unitOfWork.GetRepository<Chef>().GetByIdAsync(chefId);
        if (chef == null)
        {
            throw new ChefNotFoundException();
        }

        var parsedDate = ConvertStringToDateTime(date);
        if (!chef.AvailableDates.Contains(parsedDate))
        {
            throw new DateNotFoundException();
        }
        
        var dates = chef.AvailableDates;
        dates.Remove(parsedDate);
        chef.AvailableDates = dates;

        chef.AvailableDatesJson = JsonConvert.SerializeObject(chef.AvailableDates);
        await _unitOfWork.GetRepository<Chef>().UpdateAsync(chef);
        await _unitOfWork.CompleteAsync();
    }
    
    public async Task<List<Chef>> GetAllChefsAsync(bool asNoTracking = false)
    {
        var chefs = await _unitOfWork.GetRepository<Chef>().GetAllAsync(asNoTracking);
        return chefs.ToList();
    }

    private static DateTime ConvertStringToDateTime(string date)
    {
        var dateTimeOffset = DateTimeOffset.Parse(date);
        return dateTimeOffset.DateTime;
    }
    
    private static DateDto ConvertToDateDtoFirstFormat(string isoDate)
    {
        Console.WriteLine("Converting date" + isoDate);
        DateTime parsedDate;
        if(DateTime.TryParseExact(isoDate, "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out parsedDate))
        {
            return new DateDto
            {
                Day = parsedDate.Day,
                Month = parsedDate.Month,
                Year = parsedDate.Year
            };
        }
        else
        {
            throw new ArgumentException("Invalid date format");
        }
    }
    
    public static DateDto ConvertToDateDtoSecondFormat(string isoDate)
    {
        Console.WriteLine("Converting date" + isoDate);
        if (DateTime.TryParse(isoDate, out var parsedDate))
        {
            return new DateDto
            {
                Day = parsedDate.Day,
                Month = parsedDate.Month,
                Year = parsedDate.Year
            };
        }
        else
        {
            throw new ArgumentException("Invalid date format");
        }
    }
}