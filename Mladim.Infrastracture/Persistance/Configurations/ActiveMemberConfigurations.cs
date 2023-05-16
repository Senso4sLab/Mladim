using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Infrastracture.Persistance.Configurations;

public class ActiveMemberConfigurations : IEntityTypeConfiguration<ActiveMember>
{
    public void Configure(EntityTypeBuilder<ActiveMember> builder)
    {
        builder.HasOne(m => m.Activity)
            .WithMany(a => a.ActiveMember)
            .HasForeignKey(d => d.ActivityId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.ClientCascade);


        builder.HasOne(m => m.Project)
           .WithMany(p => p.ActiveMembers)
           .HasForeignKey(d => d.ProjectId)
           .IsRequired(false)
           .OnDelete(DeleteBehavior.ClientCascade);
    }
}
