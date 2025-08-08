using System.Security.Claims;
using GolfingDataAccessLib.Models;

namespace GolfingDataAccessLib.Logic;

public interface ISwingAnalysisProcessor
{
    Task CreateSwingAnalysisScheduleAsync(SwingAnalysisSchedule schedule, ClaimsPrincipal? user);
    Task ScheduleFittingRequestAsync(FittingRequest fittingRequest, ClaimsPrincipal user);
    Task SetSwingAnalysisScheduleAsCompletedAsync(Guid scheduleId, ClaimsPrincipal user);
}