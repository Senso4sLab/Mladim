using AutoMapper;
using Mladim.Application.Features.Members.Partners.Commands.AddPartner;
using Mladim.Application.Features.Members.StaffMembers.Commands.AddStaffMember;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.MappingProfiles.Converters;

public class AddCommandToOrganizationStaffMemberConverter : ITypeConverter<AddStaffMemberCommand, OrganizationMember>
{
    public OrganizationMember Convert(AddStaffMemberCommand source, OrganizationMember destination, ResolutionContext context) =>
     new OrganizationMember
       {
           Member = context.Mapper.Map<StaffMember>(source),
       };
}
