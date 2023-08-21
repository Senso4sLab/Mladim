using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mladim.Domain.Dtos.Members;


[JsonDerivedType(typeof(StaffMemberDetailsQueryDto))]
[JsonDerivedType(typeof(PartnerQueryDetailsDto))]
[JsonDerivedType(typeof(ParticipantDetailsQueryDto))]
public class NamedEntityDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;


    public static NamedEntityDto Create(int id, string fullName) =>
        new NamedEntityDto
        {
            Id = id,
            FullName = fullName
        };

}
