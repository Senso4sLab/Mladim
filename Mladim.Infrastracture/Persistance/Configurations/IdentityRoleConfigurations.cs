using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mladim.Domain.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Infrastracture.Persistance.Configurations;

public class IdentityRoleConfigurations : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData
        (
            new IdentityRole { Id = "7c42fd93-6681-4aaf-b3c2-1bae5841fb35", Name = ApplicationRoles.Manager, NormalizedName = ApplicationRoles.Manager.ToUpper() },
            new IdentityRole { Id = "8e5b8f54-5e4d-4398-a95a-c792c2432089", Name = ApplicationRoles.Worker, NormalizedName = ApplicationRoles.Worker.ToUpper() },
            new IdentityRole { Id = "e42e543b-5b99-40ab-b9b3-3a209eca23fd", Name = ApplicationRoles.Volunteer, NormalizedName = ApplicationRoles.Volunteer.ToUpper() }
        );
    }
}





