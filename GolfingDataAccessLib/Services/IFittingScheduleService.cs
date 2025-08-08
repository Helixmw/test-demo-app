using System.Security.Claims;
using GolfingDataAccessLib.DTOs.Fitting;
using GolfingDataAccessLib.DTOs.User;
using GolfingDataAccessLib.Enums;
using GolfingDataAccessLib.Models;
using GolfingDataAccessLib.Utilities;

namespace GolfingDataAccessLib.Services;


public interface IFittingScheduleService
{
    Task AddAsync(GetFittingDTO getFittingDTO);
    Task DeleteAsync(FittingSchedule schedule, ClaimsPrincipal? user);
    Task EditAsync(FittingSchedule schedule, ClaimsPrincipal? user);
    Task<IEnumerable<FittingSchedule>> GetAllAsync(ListParameters listParams, ClaimsPrincipal? user);
    Task<IEnumerable<FittingRequest>> GetAllFittingRequestsAsync(ClaimsPrincipal user, ListParameters listParams);
    Task<IEnumerable<ScheduleStatus>> GetAllSchedulesProgressAsync(ClaimsPrincipal user, ListParameters listParams);
    Task SetFittingScheduleStatusAsync(Guid ScheduleId, ProgressStatus progressStatus, ClaimsPrincipal user);

    Task<List<FittingSchedule>> GetAllFittingsAsync(ListParameters listParams, GetUserDTO userDTO);

    Task EditFittingScheduleAsync(FittingSchedule schedule, GetUserDTO user);

    Task<FittingSchedule> GetFittingByIdAsync(Guid scheduleId, GetUserDTO userDTO);

    Task DeleteFittingByIdAsync(Guid scheduleId, GetUserDTO userDTO);
}