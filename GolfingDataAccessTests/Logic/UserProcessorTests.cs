using System.Security.Claims;
using Autofac.Extras.Moq;
using GolfingDataAccessLib.Data;
using GolfingDataAccessLib.DTOs.User;
using GolfingDataAccessLib.Logic.Auth;
using GolfingDataAccessLib.Services.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Microsoft.EntityFrameworkCore.InMemory;


namespace GolfingDataAccessTests.Logic;
/// <summary>
/// Unit Tests
/// </summary>
public class UserProcessorTest
{
    [Fact]
    public async Task LogIn_Test()
    {
        //   using (AutoMock mock = AutoMock.GetLoose())
        // {
        //     LoginDTO user = LoginUser();

        //     var mockDbContextOptions = new DbContextOptionsBuilder<ApplicationDBContext>()
        //         .UseInMemoryDatabase(databaseName: "TestDatabase")
        //         .Options;

        //     var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
        //     var mockUserManager = new Mock<UserManager<ApplicationUser>>(
        //         mockUserStore.Object, null, null, null, null, null, null, null, null);

        //     var mockSignInManager = new Mock<SignInManager<ApplicationUser>>(
        //         mockUserManager.Object, 
        //         new Mock<IHttpContextAccessor>().Object, 
        //         new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object, 
        //         null, null, null, null);

        //         var mockRoleStore = new Mock<IRoleStore<IdentityRole>>();
        //     var mockRoleManager = new Mock<RoleManager<IdentityRole>>(
        //         mockRoleStore.Object, null, null, null, null);

        //     mock.Mock<IUserService>()
        //         .Setup(x => x.LoginAsync(user,null))
        //         .ReturnsAsync(sampleUser());

        //     var cls = mock.Create<UserProcessor>();
        //     ClaimsPrincipal res = await cls.LoginAsync(user,null);

        //     Assert.NotNull(res);
        //     Assert.Equal("JimDoe", res.Identity.Name);
           
        // }
        
    }

    private LoginDTO LoginUser(){
        return new LoginDTO{
            Email = "jim@doe.com",
            Password = "Abc123#!"
        };
    }

    private ClaimsPrincipal sampleUser(){
        ClaimsIdentity identity =  new ClaimsIdentity(new []{
            new Claim(ClaimTypes.Name, "JimDoe"),
            new Claim(ClaimTypes.Email, "jim@doe.com"),
            new Claim(ClaimTypes.Role, "Abc123#!"),
           });
           return new ClaimsPrincipal(identity);
    }
}