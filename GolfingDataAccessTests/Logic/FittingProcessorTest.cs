using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.Moq;
using GolfingDataAccessLib.Data;
using GolfingDataAccessLib.Logic;
using GolfingDataAccessLib.Models;
using GolfingDataAccessLib.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Autofac.Core;
using System.Security.Claims;
using GolfingDataAccessLib.Enums;
using GolfingDataAccessLib.Utilities;

namespace GolfingDataAccessTests.Logic;
/// <summary>
/// Unit Tests
/// </summary>
public class FittingProcessorTest
{

    [Fact]
    public void CreateFittingScheduleAsync_Test(){
        // using(var mock = AutoMock.GetLoose()){
            
        //     mock.Mock<DbContextOptions<ApplicationDBContext>>();
        //     mock.Mock<IFittingScheduleService>()
        //             .Setup(x => x.AddAsync(SampleSchedule(),SampleClaims(Roles.Customer)))
        //             .Returns(Task.CompletedTask);

        //     var cls = mock.Create<FittingProcessor>();
        //     var res = cls.CreateFittingScheduleAsync(SampleClaims(Roles.Customer),SampleSchedule());
           
        
        // }
    }

    [Fact]

    public async Task ListFittingScheduleProgressAsync_Test(){

          using(var mock = AutoMock.GetLoose()){
            var SampleSchedulesList = SampleScheduleStatusList();
            
            mock.Mock<DbContextOptions<ApplicationDBContext>>();
            mock.Mock<IFittingScheduleService>()
                    .Setup(x => x.GetAllSchedulesProgressAsync(SampleClaims(Roles.Customer),SampleParams()))
                    .Returns(Task.FromResult(SampleSchedulesList));

            var cls = mock.Create<FittingProcessor>();
            var res = await cls.ListFittingScheduleProgressAsync(SampleClaims(Roles.Customer),new ListParameters());
            Assert.NotNull(res);      
        
        }
    }

    [Fact]
    public void UpdateFittingScheduleAsync_Test()
    {
         using(var mock = AutoMock.GetLoose()){
            var SampleSchedulesList = SampleScheduleStatusList();
            
            mock.Mock<DbContextOptions<ApplicationDBContext>>();
            mock.Mock<IFittingScheduleService>()
                    .Setup(x => x.EditAsync(SampleSchedule(),SampleClaims(Roles.Customer)));
                    

            var cls = mock.Create<FittingProcessor>();
            var res = cls.UpdateFittingScheduleAsync(SampleClaims(Roles.Customer),SampleSchedule());           
        
        }
    }

    // [Fact]
    // public void DeleteFittingScheduleAsync_Test()
    // {
    //      using(var mock = AutoMock.GetLoose()){
    //         var SampleSchedulesList = SampleScheduleStatusList();
            
    //         mock.Mock<DbContextOptions<ApplicationDBContext>>();
    //         mock.Mock<IFittingScheduleService>()
    //                 .Setup(x => x.DeleteAsync(SampleSchedule(),SampleClaims(Roles.Customer)));
                    

    //         var cls = mock.Create<FittingProcessor>();
    //         var res = cls.DeleteFittingScheduleAsync(SampleClaims(Roles.Customer),SampleSchedule());           
        
    //     }
    // }

    private IEnumerable<ScheduleStatus> SampleScheduleStatusList(){
        return new List<ScheduleStatus>(){
            new ScheduleStatus{
                ScheduleId = Guid.NewGuid().ToString(),
                AvaliableDate = DateTime.Now,
                AvailableTime = "12:00 PM",
                Status = ProgressStatus.Submitted.ToString(),
            },
             new ScheduleStatus{
                ScheduleId = Guid.NewGuid().ToString(),
                AvaliableDate = DateTime.Now,
                AvailableTime = "12:00 PM",
                Status = ProgressStatus.Submitted.ToString(),
            },
             new ScheduleStatus{
                ScheduleId = Guid.NewGuid().ToString(),
                AvaliableDate = DateTime.Now,
                AvailableTime = "12:00 PM",
                Status = ProgressStatus.Submitted.ToString(),
            },

        };
    }

    private ListParameters SampleParams(){
        return new ListParameters();
    }

    private FittingSchedule SampleSchedule(){
        return new FittingSchedule{
            UserId = "og7MDCOoAtxTxV4jXliw",
                AvailableTime = "15:05",
                AvailableDate = DateTime.Now,
        };
    }

    private ClaimsPrincipal SampleClaims(Roles role){
        var user =  new ClaimsPrincipal(new ClaimsIdentity(new []{
            new Claim(ClaimTypes.Name, "JohnDoe"),
            new Claim(ClaimTypes.Email, "john@doe.com"),
           }));
        switch(role){
            case Roles.Admin:
                 user.AddIdentity(new ClaimsIdentity(new []{
            new Claim(ClaimTypes.Role, Roles.Admin.ToString()),
            }));
            break;
            case Roles.Customer:
                 user.AddIdentity(new ClaimsIdentity(new []{
                    new Claim(ClaimTypes.Role, Roles.Admin.ToString())
                    }));
            break;
        }
            return user;
          
    }
    private List<FittingSchedule> FittingSchedules(){
        return new List<FittingSchedule> {
            new FittingSchedule{
                ScheduleId = Guid.NewGuid(),
                UserId = "og7MDCOoAtxTxV4jXliw",
                AvailableTime = "15:05",
                AvailableDate = DateTime.Now,
            },
              new FittingSchedule{
                ScheduleId = Guid.NewGuid(),
                UserId = "sGMLdnBttByECaN7Yao9",
                AvailableTime = "12:45",
                AvailableDate = DateTime.Now,
            },
              new FittingSchedule{
                ScheduleId = Guid.NewGuid(),
                UserId = "atVK38TndewVsEtbmvn6",
                AvailableTime = "02:18",
                AvailableDate = DateTime.Now,
            },
        };
    }
}