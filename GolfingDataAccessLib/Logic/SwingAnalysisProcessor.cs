
using System.Security.Claims;
using GolfingDataAccessLib.Exceptions;
using GolfingDataAccessLib.Models;
using GolfingDataAccessLib.Services;

namespace GolfingDataAccessLib.Logic;
/// <summary>
/// This processor handles swing analysis operations
/// </summary>
public class SwingAnalysisProcessor : ISwingAnalysisProcessor
{

private readonly ISwingAnalysisScheduleService _swingAnalysisScheduleService;
    public SwingAnalysisProcessor(ISwingAnalysisScheduleService swingAnalysisScheduleService)
    {       
        _swingAnalysisScheduleService = swingAnalysisScheduleService;
    }

    //Create new swing analysis schedule
    public async Task CreateSwingAnalysisScheduleAsync(SwingAnalysisSchedule schedule, ClaimsPrincipal? user)
    {
        try{
            await _swingAnalysisScheduleService.AddAsync(schedule, user);
        }catch(Exception){
            throw new ScheduleOperationException("Cannot create a new swing analysis schedule. Please try later", "Swing Schedule Problem");
        }
    }

    //Schedule a 'fitting request' from a swing analysis schedule
    public async Task ScheduleFittingRequestAsync(FittingRequest fittingRequest, ClaimsPrincipal user)
    {
         try{
            await _swingAnalysisScheduleService.ScheduleSwingAnalysis(fittingRequest, user);
        }catch(Exception){
             throw new ScheduleOperationException("Cannot schedule this fitting request. Please try later", "Fitting Request Problem");
        }
    }

    //Set swing analysis schedule as completed
    public async Task SetSwingAnalysisScheduleAsCompletedAsync(Guid scheduleId, ClaimsPrincipal user)
    {
         try{
            await _swingAnalysisScheduleService.SetSwingAnalysisScheduleAsCompleted(scheduleId, user);
        }catch(Exception){
             throw new ScheduleOperationException("Cannot mark this swing analysis schedule as complete. Please try later", "Mark Complete Problem");
        }
    }
}