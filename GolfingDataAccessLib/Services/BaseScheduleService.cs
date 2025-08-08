using System.Security.Claims;
using GolfingDataAccessLib.Data;
using GolfingDataAccessLib.Exceptions;
using GolfingDataAccessLib.interfaces;
using GolfingDataAccessLib.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GolfingDataAccessLib.Services;

/// <summary>
/// Base schedule class for different types of schedules with the same base operations.
/// It also injects the Database Context to be used inside the base and derived services.
/// </summary>
public partial class BaseScheduleService<T>  where T : class, ISchedule
{
    protected readonly ApplicationDBContext _dbContext;
    protected readonly UserManager<ApplicationUser> _userManager;

    public BaseScheduleService(UserManager<ApplicationUser> userManager,
                                ApplicationDBContext dbContext)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    //Generic Implementations that apply to both
    //schedule types (Fitting and SwingAnalysis)
    // with type 'T'
    
    //Add schedule
    public virtual async Task AddAsync(T schedule, ClaimsPrincipal? user = null)
    {
       await  _dbContext.Set<T>().AddAsync(schedule);
        await SaveChangesAsync();
    }

     //Edit schedule
    public virtual async Task EditAsync(T schedule, ClaimsPrincipal? user = null)
    {
        _dbContext.Set<T>().Update(schedule);
        await SaveChangesAsync();
    }

     //Delete schedule
    public virtual async Task DeleteAsync(T schedule, ClaimsPrincipal? user = null)
    {
        _dbContext.Set<T>().Remove(schedule);
        await SaveChangesAsync();
    }

    //Get all schedules
    public virtual async Task<IEnumerable<T>> GetAllAsync(ListParameters parameters, ClaimsPrincipal? user = null)
    {
        var dbSet = await _dbContext.Set<T>().ToListAsync();
        return dbSet;      
    }

    //Get all of a special type 'U'
    public async Task<IEnumerable<U>> GetAllAsync<U>() where U : class
    {
        var dbSet = await _dbContext.Set<U>().ToListAsync();
        return dbSet;
    }

    //Get a schedule by id
    public async Task<T> GetAsync(string id)
    {
       var dbSet = await _dbContext.Set<T>().FindAsync(id);
       return dbSet;
    }

    //Save Changes
    protected async Task SaveChangesAsync(){
         await _dbContext.SaveChangesAsync();
    }

    protected async Task<string> ExtractUserIdAsync(ClaimsPrincipal user){
        var email = user?.FindFirst(ClaimTypes.Email)?.Value;
        var _user = await _userManager.FindByEmailAsync(email);
        if(_user is null)
        throw new ObjectNotFoundException("Unable to find your user information. Please try again later");

        return _user.Id;
    }

     protected async Task<string> ExtractUserIdWithEmailAsync(string email){
        
        var _user = await _userManager.FindByEmailAsync(email);
        if(_user is null)
        throw new ObjectNotFoundException("Unable to find your user information. Please try again later");

        return _user.Id;
    }
}