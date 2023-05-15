using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Models;
using System.Reflection;

namespace Mladim.Infrastracture.Persistance;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<ApplicationUser> AppUsers { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<StaffMember> Stuff { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<AnonymousParticipants> AnonymousParticipantGroups { get; set; }
    public DbSet<Partner> Partners { get; set; }
    public DbSet<ActiveMember> SubjectMembers { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<Group> Groups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
