using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Models;
using System.Reflection;

namespace Mladim.Infrastracture.Persistance;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<AppUser> AppUsers { get; set; }    
    public DbSet<StaffMember> Staff { get; set; }
    public DbSet<Participant> Participants { get; set; }    
    public DbSet<Partner> Partners { get; set; }    
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Activity> Activities { get; set; } 
    public DbSet<ProjectGroup> ProjectGroups { get; set; }
    public DbSet<ActivityGroup> ActivityGroups { get; set; }
    public DbSet<Group> Groups { get; set; }  
    public DbSet<AttachedFile> Files { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
