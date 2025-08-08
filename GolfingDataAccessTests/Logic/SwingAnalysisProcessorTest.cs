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
public class SwingAnalysisTest
{

    [Fact]
    public void CreateSwingAnalysisScheduleAsync_Test(){
        using(var mock = AutoMock.GetLoose()){
            
            mock.Mock<DbContextOptions<ApplicationDBContext>>();
            mock.Mock<ISwingAnalysisScheduleService>()
                    .Setup(x => x.AddAsync(SampleSchedule(),SampleClaims(Roles.Customer)))
                    .Returns(Task.CompletedTask);

            var cls = mock.Create<SwingAnalysisProcessor>();
            var res = cls.CreateSwingAnalysisScheduleAsync(SampleSchedule(),SampleClaims(Roles.Customer));
           
        
        }
    }

    [Fact]

    public void ScheduleFittingRequestAsync_Test(){

          using(var mock = AutoMock.GetLoose()){

            mock.Mock<DbContextOptions<ApplicationDBContext>>();
            mock.Mock<ISwingAnalysisProcessor>()
                    .Setup(x => x.ScheduleFittingRequestAsync(SampleRequest(),SampleClaims(Roles.Customer)))
                    .Returns(Task.FromResult(SampleScheduleStatusList()));

            var cls = mock.Create<ISwingAnalysisProcessor>();
            var res = cls.ScheduleFittingRequestAsync(SampleRequest(),SampleClaims(Roles.Customer));
            Assert.NotNull(res);      
        
        }
    }

    [Fact]
    public void SetSwingAnalysisScheduleAsCompletedAsync_Test()
    {
         using(var mock = AutoMock.GetLoose()){
           
            var scheduleId = Guid.NewGuid();
            mock.Mock<DbContextOptions<ApplicationDBContext>>();
            mock.Mock<ISwingAnalysisScheduleService>()
                    .Setup(x => x.SetSwingAnalysisScheduleAsCompleted(scheduleId,SampleClaims(Roles.Customer)));
                    

            var cls = mock.Create<SwingAnalysisProcessor>();
            var res = cls.SetSwingAnalysisScheduleAsCompletedAsync(scheduleId,SampleClaims(Roles.Customer));           
        
        }
    }



    private FittingRequest SampleRequest(){
        return new FittingRequest{
            AvailableTime = "12:00 PM",
            AvailableDate = DateTime.Now,
            Comment = "",

        };
    }

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

    private SwingAnalysisSchedule SampleSchedule(){
        return new SwingAnalysisSchedule{
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
    private List<SwingAnalysisSchedule> SwingAnalysisSchedules(){
        return new List<SwingAnalysisSchedule> {
            new SwingAnalysisSchedule{
                ScheduleId = Guid.NewGuid(),
                UserId = "og7MDCOoAtxTxV4jXliw",
                AvailableTime = "15:05",
                AvailableDate = DateTime.Now,
            },
              new SwingAnalysisSchedule{
                ScheduleId = Guid.NewGuid(),
                UserId = "sGMLdnBttByECaN7Yao9",
                AvailableTime = "12:45",
                AvailableDate = DateTime.Now,
            },
              new SwingAnalysisSchedule{
                ScheduleId = Guid.NewGuid(),
                UserId = "atVK38TndewVsEtbmvn6",
                AvailableTime = "02:18",
                AvailableDate = DateTime.Now,
            },
        };
    }
}