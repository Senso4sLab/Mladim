using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mladim.Domain.Models;

namespace Mladim.Infrastracture.Persistance.Configurations;

public class StaffMemberProjectConfigurations : IEntityTypeConfiguration<StaffMemberProject>
{
    public void Configure(EntityTypeBuilder<StaffMemberProject> builder)
    {
        builder.HasKey(smp => new { smp.StaffMemberId, smp.ProjectId, smp.IsLead });        
       
        builder.Navigation(smp => smp.StaffMember).AutoInclude();
    }
}





