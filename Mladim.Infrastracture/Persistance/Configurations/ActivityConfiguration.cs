using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mladim.Domain.Enums;
using Mladim.Domain.Models;

namespace Mladim.Infrastracture.Persistance.Configurations;

public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.OwnsMany(a => a.AnonymousParticipantGroups)
            .OwnsOne(ag => ag.AnonymousParticipant);

        builder.OwnsOne(activity => activity.TimeRange);      

        builder.OwnsOne(activity => activity.Attributes, owned =>
        {
            owned.Property(a => a.ActivityTargetGroup)
                 .HasDefaultValue(ActivityTargetGroup.Participants);
        });

        builder.OwnsMany(Activity => Activity.Files);     
    }
}
