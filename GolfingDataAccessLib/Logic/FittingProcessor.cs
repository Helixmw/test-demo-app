using System.Security.Claims;
using GolfingDataAccessLib.DTOs.Fitting;
using GolfingDataAccessLib.DTOs.User;
using GolfingDataAccessLib.Exceptions;
using GolfingDataAccessLib.Models;
using GolfingDataAccessLib.Services;
using GolfingDataAccessLib.Utilities;

namespace GolfingDataAccessLib.Logic;
/// <summary>
/// 'FittingProcessor' hands everything regarding fitting schedules
/// </summary>
public class FittingProcessor : IFittingProcessor
{
    private readonly IFittingScheduleService _fittingScheduleService;

    public FittingProcessor(IFittingScheduleService fittingScheduleService)
    {
        _fittingScheduleService = fittingScheduleService;
    }

    //Creates new Fitting Schedule
    public async Task CreateFittingScheduleAsync(GetFittingDTO getFittingDTO)
    {
        
        try{
            await _fittingScheduleService.AddAsync(getFittingDTO);
        }catch(Exception ex){
            throw new ScheduleOperationException("Something went wrong creating your schedule. Please try later", "Create Schedule Problem");
        }
    }

    //List fitting schedules
    public async Task<List<FittingSchedule>> ListFittingSchedulesAsync(ListParameters listParams, GetUserDTO user)
    {
         try{
           var scheduleStatuses = await _fittingScheduleService.GetAllFittingsAsync(listParams, user);
           return scheduleStatuses;
        }catch(Exception){
            throw new ScheduleOperationException("Unable to list fitting schedules. Please try again later", "Schedule List Problem");
        }
    }

    //List fitting schedule progress
    public async Task<IEnumerable<ScheduleStatus>> ListFittingScheduleProgressAsync(ClaimsPrincipal user,ListParameters listParams)
    {
         try{
           var scheduleStatuses = await _fittingScheduleService.GetAllSchedulesProgressAsync(user,listParams);
           return scheduleStatuses;
        }catch(Exception){
            throw new ScheduleOperationException("Unable to list fitting schedule progress. Please try again later", "Schedule List Problem");
        }
    }

    //Get fitting by id
    public async Task<FittingSchedule> GetFittingScheduleByIdAsync(Guid ScheduleId, GetUserDTO userDTO){
         try{
           var scheduleStatuses = await _fittingScheduleService.GetFittingByIdAsync(ScheduleId, userDTO);
           return scheduleStatuses;
        }catch(Exception){
            throw new ScheduleOperationException("Unable to get this schedule. Please try again later", "Schedule List Problem");
        }
    }

    

    //Updates Fitting Schedule
    public async Task UpdateFittingScheduleAsync(ClaimsPrincipal user, FittingSchedule fittingSchedule)
    {
        try{
            await _fittingScheduleService.EditAsync(fittingSchedule, user);
        }catch(Exception){
            throw new ScheduleOperationException("Something went wrong updating your schedule. Please try later", "Schedule Update Problem");
        }
       
    }

    //Edit fitting schedule
    public async Task EditFittingScheduleAsync(FittingSchedule fittingSchedule, GetUserDTO userDTO){
        try{
            await _fittingScheduleService.EditFittingScheduleAsync(fittingSchedule,userDTO);
        }catch(Exception ex){
            throw new ScheduleOperationException("Something went wrong editing your schedule. Please try later", "Edit Problem");
        }
    }

    //Deletes Fitting Schedule
    public async Task<bool> DeleteFittingScheduleAsync(Guid scheduleId, GetUserDTO userDTO)
    {
        try{
            await _fittingScheduleService.DeleteFittingByIdAsync(scheduleId, userDTO);
            return true;
        }catch(Exception){
            throw new ScheduleOperationException("Unable to delete schedule. Please try later", "Delete Schedule Problem");
        }       
        
    }


}