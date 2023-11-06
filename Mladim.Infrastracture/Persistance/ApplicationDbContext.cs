using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;
using Mladim.Client.ViewModels.Survey;
using Mladim.Domain.Enums;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Models;
using Mladim.Domain.Models.Survey.Questions;
using Mladim.Domain.Models.Survey.Responses;
using Mladim.Infrastracture.Persistance.Conversions;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Metadata;

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
    public DbSet<SurveyQuestion> Questions { get; set; }
    public DbSet<SurveyQuestionnairy> Questionnairies { get; set; }
    //public DbSet<SurveyResponse> SurveryResponses { get; set; }   


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


        //modelBuilder.Entity<QuestionRatingResponseVM>();
        //modelBuilder.Entity<QuestionTextResponseVM>();
        //modelBuilder.Entity<QuestionBooleanResponseVM>();
        //modelBuilder.Entity<QuestionMultiButtonResponseVM>();


        modelBuilder.Entity<FemaleSurveyQuestion>();
        modelBuilder.Entity<MaleSurveyQuestion>();

       

        DbSeeds.GeneratedSeeds(modelBuilder);


        base.OnModelCreating(modelBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<List<string>>().HaveConversion<SurveyQuestionConverter>();
        //configurationBuilder.Properties<List<SurveyMultipleResponseType>>().HaveConversion<SurveyMultipleResponseTypeConverter>();
        base.ConfigureConventions(configurationBuilder);
    }
}

