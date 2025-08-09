using GolfingAppUI.Authentication;
using GolfingAppUI.Exceptions;
using GolfingAppUI.Helpers;
using GolfingDataAccessLib.Authentication;
using GolfingDataAccessLib.Data;
using GolfingDataAccessLib.Logic;
using GolfingDataAccessLib.Logic.Auth;
using GolfingDataAccessLib.Services;
using GolfingDataAccessLib.Services.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthenticationCore();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<CustomAuthenticationStateComponent>();
builder.Services.AddScoped<CookieService>();
builder.Services.AddAuthentication()
                .AddScheme<CustomOptions, CustomAuthenticationHandler>(
                    builder.Configuration["AuthType"], 
                    options => { });

builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthenticationStateProvider>());
builder.Services.AddCascadingAuthenticationState();
 


//Injecting the AppDBContext from the Access Library
var connString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<ApplicationDBContext>(options => 
    options.UseNpgsql(connString, b => b.MigrationsAssembly("GolfingAppUI")));   

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddDefaultTokenProviders();


builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddScoped<SignInManager<ApplicationUser>>();
builder.Services.AddScoped<IFittingScheduleService, FittingScheduleService>();
builder.Services.AddScoped<IFittingProcessor, FittingProcessor>();
builder.Services.AddScoped<ISwingAnalysisScheduleService, SwingAnalysisScheduleService>();
builder.Services.AddScoped<ISwingAnalysisProcessor, SwingAnalysisProcessor>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserProcessor, UserProcessor>();
//builder.Services.AddScoped<SeedingService>();
builder.Services.AddCascadingAuthenticationState();



var app = builder.Build();

//Scope created to start data seeding
using(var scope = app.Services.CreateScope()){
    var services = scope.ServiceProvider;
    try{

        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
         var configuration = services.GetRequiredService<IConfiguration>();
        //Seed database
        await Seeders.SeedRolesAndDefaultUser(roleManager, userManager, configuration);
        
    }catch(Exception ex)
    {
        throw new DatabaseOperationException($"Something went wrong starting up service. Please try again later: {ex.Message}");
    }
}

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();  


app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
