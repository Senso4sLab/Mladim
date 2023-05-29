using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mladim.Domain.Models;

namespace Mladim.Infrastracture.Persistance.Configurations;

public class StaffMemberActivityConfigurations : IEntityTypeConfiguration<StaffMemberActivity>
{
    public void Configure(EntityTypeBuilder<StaffMemberActivity> builder)
    {
        builder.HasKey(p => new { p.StaffMemberId, p.ActivityId, p.IsLead });
    }
}





