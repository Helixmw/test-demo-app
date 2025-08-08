using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using GolfingDataAccessLib.Data;
using GolfingDataAccessLib.Enums;
using GolfingDataAccessLib.Exceptions;
using GolfingDataAccessLib.Models;
using GolfingDataAccessLib.Utilities;

namespace GolfingDataAccessLib.Services;
/// <summary>
/// Admin implementations for FittingScheduleService
/// </summary>
public partial class FittingScheduleService{
    
   

    //Fitting Requests(Admin)
    //Gets all fitting requests by users for the Admin
    public async Task<IEnumerable<FittingRequest>> GetAllFittingRequestsAsync(ClaimsPrincipal user, ListParameters listParams){
        VerifyUserRole(user, Roles.Admin); 
        var schedules = GetFittingRequests(listParams);
        var customers = await GetAllCustomerUsers();
        var fittingRequests = schedules.Join(customers,
                                            x => x.UserId,
                                            y => y.Id,
                                            (x,y) => new FittingRequest{
                                                UserId = y.Id,
                                                UserName = y.UserName ?? String.Empty,
                                                Email = y.Email ?? String.Empty,
                                                AvailableTime = x.AvailableTime,
                                                AvailableDate = x.AvailableDate,
                                                Comment = x.Comment ?? String.Empty,
                                                IsAcknowledged = x.IsAcknowledged,
                                            }).ToList();
            return fittingRequests;             
    }

    //Updating fitting schedule status
    public async Task SetFittingScheduleStatusAsync(Guid ScheduleId, ProgressStatus progressStatus, ClaimsPrincipal user)
    {
    VerifyUserRole(user, Roles.Admin); 
       CheckFittingScheduleExists(ScheduleId);
       var status = _dbContext.Set<FittingStatus>().FirstOrDefault(x => x.ScheduleId == ScheduleId);
       status.Status = progressStatus.ToString();
       _dbContext.Set<FittingStatus>().Update(status);
       await SaveChangesAsync();

    }



    //Gets all fitting schedules
    private IEnumerable<FittingSchedule> GetFittingRequests(ListParameters listParams){
         return _dbContext.Set<FittingSchedule>()
                        .Skip((listParams.PageNumber - 1) * listParams.PageSize) 
                        .Take(listParams.PageSize) //Paginated collection
                        .ToList();
    }

    //Gets all 'Customer' users
    private async Task<IEnumerable<ApplicationUser>> GetAllCustomerUsers(){
        return await _userManager.GetUsersInRoleAsync(Roles.Customer.ToString());

    }
}