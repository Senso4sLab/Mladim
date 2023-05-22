using AutoMapper;
using MediatR;
using Mladim.Application.Features.Members.Participants.Commands.AddParticipant;
using Mladim.Application.Features.Members.StaffMembers.Commands.AddStaffMember;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.MappingProfiles.Converters;


public class AddCommandToOrganizationParticipantConverter : ITypeConverter<AddParticipantCommand, OrganizationMember>
{
    public OrganizationMember Convert(AddParticipantCommand source, OrganizationMember destination, ResolutionContext context) =>
        new OrganizationMember
        {
            Member = context.Mapper.Map<Participant>(source),
        };
}


