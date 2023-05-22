using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mladim.Domain.Models;

namespace Mladim.Infrastracture.Persistance.Configurations;

public class OrganizationMemberConfigurations : IEntityTypeConfiguration<OrganizationMember>
{
    public void Configure(EntityTypeBuilder<OrganizationMember> builder)
    {
        builder.HasKey(p => new { p.OrganizationId, p.MemberId });
    }
}





