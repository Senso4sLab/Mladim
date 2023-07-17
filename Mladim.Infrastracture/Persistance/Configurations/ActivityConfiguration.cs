using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Infrastracture.Persistance.Configurations;

public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.OwnsMany(a => a.AnonymousParticipantGroups)
            .OwnsOne(ag => ag.AnonymousParticipant);

        builder.OwnsOne(activity => activity.TimeRange);
        builder.OwnsOne(activity => activity.Attributes);

     
    }
}
