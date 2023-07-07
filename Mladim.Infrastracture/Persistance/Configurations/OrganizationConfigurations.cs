using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Infrastracture.Persistance.Configurations;

public class OrganizationConfigurations : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.OwnsOne(organization => organization.SocialMediaUrls);

        builder.HasMany("partners").WithOne("Organization");
        builder.HasMany("members").WithOne("Organization");
        builder.HasMany("groups").WithOne("Organization");
    }
}
