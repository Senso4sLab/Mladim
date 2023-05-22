using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mladim.Domain.Models;

namespace Mladim.Infrastracture.Persistance.Configurations;

public class OrganizationPartnerConfigurations : IEntityTypeConfiguration<OrganizationPartner>
{
    public void Configure(EntityTypeBuilder<OrganizationPartner> builder)
    {
        builder.HasKey(p => new { p.OrganizationId, p.PartnerId });
    }
}





