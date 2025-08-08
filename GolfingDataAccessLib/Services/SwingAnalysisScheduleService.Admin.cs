using System.Formats.Asn1;
using System.Security.Claims;
using GolfingDataAccessLib.Data;
using GolfingDataAccessLib.Enums;
using GolfingDataAccessLib.Exceptions;
using GolfingDataAccessLib.Models;
using GolfingDataAccessLib.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GolfingDataAccessLib.Services;
/// <summary>
/// 'SwingAnalysisScheduleService' is derived from 'BaseSchedule' service taking in a type 'T' of ISchedule
/// </summary>
public partial class SwingAnalysisScheduleService
{
    //Sets Swing Analysis Schedule Status
    public async Task SetSwingAnalysisScheduleAsCompleted(Guid scheduleId, ClaimsPrincipal user){
        VerifyUserRole(user,Roles.Admin);
        await CheckSwingScheduleExistsAsync(scheduleId);
        var schedule = await _dbContext.Set<SwingAnalysisSchedule>().FirstOrDefaultAsync(x => x.ScheduleId == scheduleId);
        schedule.Status = ProgressStatus.Completed.ToString();
        _dbContext.Set<SwingAnalysisSchedule>().Update(schedule);
        await SaveChangesAsync();
    }


    private void CheckFittingScheduleExists(Guid scheduleId){
        var schedule = _dbContext.Set<FittingSchedule>().FirstOrDefault(x => x.ScheduleId == scheduleId);
        if (schedule is null)
        throw new ObjectNotFoundException("This fitting schedule does not exist");
    }


}
