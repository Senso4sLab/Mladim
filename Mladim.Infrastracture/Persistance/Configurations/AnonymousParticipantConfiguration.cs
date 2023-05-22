using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mladim.Domain.Enums;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Infrastracture.Persistance.Configurations;

public class AnonymousParticipantConfiguration : IEntityTypeConfiguration<AnonymousParticipant>
{
    public void Configure(EntityTypeBuilder<AnonymousParticipant> builder)
    {
        int id = 1;

        foreach(var ageGroup in  Enum.GetValues<AgeGroups>())
        {
            foreach (var gender in Enum.GetValues<Gender>())
            {
                builder.HasData(new AnonymousParticipant
                {
                    Id = id++,
                    AgeGroup = ageGroup,
                    Gender   = gender,
                });
            }
        }        
    }
}
