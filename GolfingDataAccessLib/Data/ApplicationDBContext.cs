using System.Security;
using System.Security.Cryptography.X509Certificates;
using GolfingDataAccessLib.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GolfingDataAccessLib.Data;

public class ApplicationDBContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {
    
    }
    public DbSet<SwingAnalysisSchedule> AnalysisSchedules { get; set; }

    public DbSet<FittingSchedule> FittingSchedules{ get; set; }

}