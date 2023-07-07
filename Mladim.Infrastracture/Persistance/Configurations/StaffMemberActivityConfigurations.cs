using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mladim.Domain.Models;

namespace Mladim.Infrastracture.Persistance.Configurations;

public class StaffMemberActivityConfigurations : IEntityTypeConfiguration<StaffMemberActivity>
{
    public void Configure(EntityTypeBuilder<StaffMemberActivity> builder)
    {
        builder.HasKey(sma => new { sma.StaffMemberId, sma.ActivityId, sma.IsLead });

        builder.HasOne(sma => sma.StaffMember).WithMany(s => s.StaffActivities);
        builder.HasOne(sma => sma.Activity).WithMany("staff");

        builder.Navigation(smp => smp.StaffMember).AutoInclude();
    }
}





