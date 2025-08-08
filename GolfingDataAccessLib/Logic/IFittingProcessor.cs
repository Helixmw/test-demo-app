using System.Security.Claims;
using GolfingDataAccessLib.DTOs.Fitting;
using GolfingDataAccessLib.DTOs.User;
using GolfingDataAccessLib.Models;
using GolfingDataAccessLib.Utilities;

namespace GolfingDataAccessLib.Logic;

public interface IFittingProcessor
{
    Task CreateFittingScheduleAsync(GetFittingDTO getFittingDTO);
    Task<IEnumerable<ScheduleStatus>> ListFittingScheduleProgressAsync(ClaimsPrincipal user, ListParameters listParams);
    Task UpdateFittingScheduleAsync(ClaimsPrincipal user, FittingSchedule fittingSchedule);

    Task<bool> DeleteFittingScheduleAsync(Guid scheduleId, GetUserDTO userDTO);

    Task<List<FittingSchedule>> ListFittingSchedulesAsync(ListParameters listParams, GetUserDTO user);

    Task EditFittingScheduleAsync(FittingSchedule fittingSchedule, GetUserDTO userDTO);

    Task<FittingSchedule> GetFittingScheduleByIdAsync(Guid ScheduleId, GetUserDTO userDTO);
}
