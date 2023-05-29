using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mladim.Domain.Models;

namespace Mladim.Infrastracture.Persistance.Configurations;

public class StaffMemberProjectConfigurations : IEntityTypeConfiguration<StaffMemberProject>
{
    public void Configure(EntityTypeBuilder<StaffMemberProject> builder)
    {
        builder.HasKey(p => new { p.StaffMemberId, p.ProjectId, p.IsLead });
    }
}





