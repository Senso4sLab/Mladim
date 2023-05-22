using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mladim.Domain.Models;

namespace Mladim.Infrastracture.Persistance.Configurations;

public class OrganizationgroupConfigurations : IEntityTypeConfiguration<OrganizationGroup>
{
    public void Configure(EntityTypeBuilder<OrganizationGroup> builder)
    {
        builder.HasKey(p => new { p.OrganizationId, p.GroupId });
    }
}









