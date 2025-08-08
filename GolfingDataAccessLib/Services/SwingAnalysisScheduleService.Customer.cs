using System.Security.Claims;
using System.Threading.Tasks;
using GolfingDataAccessLib.Data;
using GolfingDataAccessLib.Enums;
using GolfingDataAccessLib.Exceptions;
using GolfingDataAccessLib.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GolfingDataAccessLib.Services;

/// <summary>
/// 'SwingAnalysisScheduleService' is derived from 'BaseSchedule' service taking in a type 'T' of ISchedule
/// </summary>
public partial class SwingAnalysisScheduleService : BaseScheduleService<SwingAnalysisSchedule>, ISwingAnalysisScheduleService
{
    public SwingAnalysisScheduleService(
        UserManager<ApplicationUser> userManager,
        ApplicationDBContext dbContext) : base(userManager, dbContext)
    {

    }

    //Schedules a swing analysis
    //Special function to add swing analysis schedule
    public override async Task AddAsync(SwingAnalysisSchedule schedule, ClaimsPrincipal? user)
    {
        VerifyUserRole(user, Roles.Customer);
        await base.AddAsync(schedule);
        await SaveChangesAsync();

    }

    //Schedule A Swing Analysis 
    //Takes in a 'FittingRequest' listed on the user side and then makes a swing anaylsis schedule
    public async Task ScheduleSwingAnalysis(FittingRequest fittingRequest, ClaimsPrincipal user)
    {
        VerifyUserRole(user, Roles.Customer);
        CheckFittingScheduleExists(fittingRequest.ScheduleId);
        await _dbContext.Set<SwingAnalysisSchedule>().AddAsync(
           new SwingAnalysisSchedule
           {
               AvailableTime = fittingRequest.AvailableTime,
               AvailableDate = fittingRequest.AvailableDate,
               Comment = fittingRequest.Comment,
               UserId = fittingRequest.UserId,
               Status = ProgressStatus.Scheduled.ToString(),
           }
        );
        await SaveChangesAsync();

    }

    //Checks if Swing Schedule exists
    private async Task CheckSwingScheduleExistsAsync(Guid scheduleId)
    {
        var schedule = await _dbContext.Set<SwingAnalysisSchedule>().FirstOrDefaultAsync(x => x.ScheduleId == scheduleId);
        if (schedule is null)
            throw new ObjectNotFoundException("This schedule does not exist");
    }

}