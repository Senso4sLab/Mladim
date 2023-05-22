using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mladim.Domain.Models;

namespace Mladim.Infrastracture.Persistance.Configurations;

public class AnonymousParticipantActivityConfigurations : IEntityTypeConfiguration<AnonymousParticipantActivity>
{
    public void Configure(EntityTypeBuilder<AnonymousParticipantActivity> builder)
    {
        builder.HasKey(p => new { p.AnonymousParticipantId, p.ActivityId });
    }
}









