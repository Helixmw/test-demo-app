using System.Security.Claims;
using GolfingDataAccessLib.Models;

namespace GolfingDataAccessLib.Services;

public interface ISwingAnalysisScheduleService
{
    Task AddAsync(SwingAnalysisSchedule schedule, ClaimsPrincipal? user);
    Task ScheduleSwingAnalysis(FittingRequest fittingRequest, ClaimsPrincipal user);
    Task SetSwingAnalysisScheduleAsCompleted(Guid scheduleId, ClaimsPrincipal user);
}