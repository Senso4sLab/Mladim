using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mladim.Domain.Enums;

namespace Mladim.Infrastracture.Persistance.Configurations;

//public class RoleConfigurations : IEntityTypeConfiguration<IdentityRole>
//{
//    public void Configure(EntityTypeBuilder<IdentityRole> builder)
//    {
//        foreach (var role in Enum.GetNames<ApplicationRole>())
//            builder.HasData(new IdentityRole(role));

//    }
//}
