using System.Security.Claims;
using GolfingDataAccessLib.Data;
using GolfingDataAccessLib.DTOs.Fitting;
using GolfingDataAccessLib.DTOs.User;
using GolfingDataAccessLib.Enums;
using GolfingDataAccessLib.Exceptions;
using GolfingDataAccessLib.Models;
using GolfingDataAccessLib.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GolfingDataAccessLib.Services;

/// <summary>
/// 'FittingScheduleService' is derived from 'BaseSchedule' service taking in a type 'T' of ISchedule
/// </summary>
public partial class FittingScheduleService : BaseScheduleService<FittingSchedule>, IFittingScheduleService
{

    public FittingScheduleService(
        UserManager<ApplicationUser> userManager,
        ApplicationDBContext dbContext) : base(userManager, dbContext)
    {

    }
    //Schedule a fitting
    //Special function to add fitting schedule and progress
    public async Task AddAsync(GetFittingDTO getFittingDTO)
    {
        VerifyDTOUserRole(getFittingDTO, Roles.Customer);
        var id = await ExtractUserIdWithEmailAsync(getFittingDTO.Email);
        var schedule = new FittingSchedule
        {
            UserId = id,
            AvailableDate = getFittingDTO.AvailableDate,
            AvailableTime = getFittingDTO.AvailableTime.ToString(),
            Comment = getFittingDTO.Comment,
            Status = ProgressStatus.Submitted.ToString()
        };
        await _dbContext.FittingSchedules.AddAsync(schedule);
        await _dbContext.SaveChangesAsync();
        await UpdateFittingProgress(schedule);
    }

    //Get fitting by Id
    public async Task<FittingSchedule> GetFittingByIdAsync(Guid scheduleId, GetUserDTO userDTO){
        VerifyDTOUserRole(userDTO,Roles.Customer);
        var id = await ExtractUserIdWithEmailAsync(userDTO.Email);
        var schedule = await _dbContext.FittingSchedules.Where(x => x.UserId == id)
                                                    .Where(x => x.ScheduleId == scheduleId)
                                                    .FirstOrDefaultAsync();
        if(schedule is null)
        throw new ScheduleOperationException("This fitting schedule was not found", "Schedule not found");

        schedule.UserId = string.Empty;
        return schedule;
    }

    //delete fitting
    public async Task DeleteFittingByIdAsync(Guid scheduleId, GetUserDTO userDTO){
        VerifyDTOUserRole(userDTO,Roles.Customer);
        var id = await ExtractUserIdWithEmailAsync(userDTO.Email);
        var schedule = await _dbContext.FittingSchedules.Where(x => x.UserId == id)
                                                    .Where(x => x.ScheduleId == scheduleId)
                                                    .FirstOrDefaultAsync();
        if(schedule is null)
         throw new ScheduleOperationException("This fitting schedule was not found", "Schedule not found");

         _dbContext.FittingSchedules.Remove(schedule);
         await SaveChangesAsync();
    }

    //Edit schedule
    public async Task EditFittingScheduleAsync(FittingSchedule schedule, GetUserDTO user)
    {
        VerifyDTOUserRole(user, Roles.Customer);
        var id = await ExtractUserIdWithEmailAsync(user.Email);
        var _sch = await _dbContext.FittingSchedules.Where(x => x.ScheduleId == schedule.ScheduleId).FirstOrDefaultAsync();
        _sch.AvailableDate = schedule.AvailableDate;
        _sch.AvailableTime = schedule.AvailableTime;
        _sch.Comment = schedule.Comment;
        _sch.UserId = id;
        _dbContext.FittingSchedules.Update(_sch);      
        await _dbContext.SaveChangesAsync();
    }

    //Delete schedule
    public override async Task DeleteAsync(FittingSchedule schedule, ClaimsPrincipal? user)
    {
        VerifyUserRole(user, Roles.Customer);
        await base.DeleteAsync(schedule);
    }

    //Get all schedules
    public async Task<List<FittingSchedule>> GetAllFittingsAsync(ListParameters listParams, GetUserDTO userDTO)
    {
         VerifyDTOUserRole(userDTO, Roles.Customer);
        var userId = await ExtractUserIdWithEmailAsync(userDTO.Email);
        var schedules = _dbContext.Set<FittingSchedule>()
                            .Where(x => x.UserId == userId)
                            .OrderByDescending(x => x)
                            .Skip((listParams.PageNumber - 1) * listParams.PageSize)
                            .Take(listParams.PageSize) //Paginated collection
                            .ToList();
        return schedules;
    }

    //Account History
    //Shows a list of all fittings and their statuses
    //Gets all fitting schedules and their progress status for a specific user with user id
    public async Task<IEnumerable<ScheduleStatus>> GetAllSchedulesProgressAsync(ClaimsPrincipal user, ListParameters listParams)
    {
        VerifyUserRole(user, Roles.Customer);
        var userId = await ExtractUserIdAsync(user);
        var fittingSchedules = GetAllSchedulesByUserId(userId, listParams);
        var fittingStatuses = await GetAllAsync<FittingStatus>();
        //Generate custom collection to show fitting schedules
        //by the user and progress status
        var scheduleStatuses = fittingSchedules.Join(fittingStatuses,
                                                    x => x.ScheduleId,
                                                    y => y.ScheduleId,
                                                    (x, y) => new ScheduleStatus
                                                    {
                                                        ScheduleId = x.ScheduleId.ToString(),
                                                        AvaliableDate = x.AvailableDate,
                                                        AvailableTime = x.AvailableTime,
                                                        Comment = x.Comment,
                                                        Status = y.Status,
                                                    })
                                                    .ToList();
        return scheduleStatuses;
    }

    //Updated newly created fitting status to 'Submitted'
    private async Task UpdateFittingProgress(FittingSchedule schedule)
    {
        schedule.Status = ProgressStatus.Submitted.ToString();
        await base.EditAsync(schedule);
    }


    //Get all fitting schedules by user Id
    private IEnumerable<FittingSchedule> GetAllSchedulesByUserId(string UserId, ListParameters listParams)
    {
        return _dbContext.Set<FittingSchedule>()
        .Where(x => x.UserId == UserId)
        .Skip((listParams.PageNumber - 1) * listParams.PageSize)
        .Take(listParams.PageSize) //Paginated collection
        .ToList();
    }

    //Checks if Fitting Schedule exists
    private void CheckFittingScheduleExists(Guid scheduleId)
    {
        var schedule = _dbContext.Set<FittingSchedule>().FirstOrDefault(x => x.ScheduleId == scheduleId);
        if (schedule is null)
            throw new ObjectNotFoundException("This schedule does not exist");
    }
}