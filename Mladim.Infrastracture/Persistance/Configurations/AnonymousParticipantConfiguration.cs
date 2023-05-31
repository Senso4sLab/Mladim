using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Mladim.Domain.Enums;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Infrastracture.Persistance.Configurations;

public class AnonymousParticipantConfiguration : IEntityTypeConfiguration<AnonymousParticipant>
{  
   
    public void Configure(EntityTypeBuilder<AnonymousParticipant> builder)
    {      

        foreach (var ageGroup in Enum.GetValues<AgeGroups>())
        {
            foreach (var gender in Enum.GetValues<Gender>())
            {
                builder.HasData(new AnonymousParticipant
                {
                    AgeGroup = ageGroup,
                    Gender = gender,
                    Id = (int)ageGroup + (int)gender,
                });
            }
        }
    }
}
