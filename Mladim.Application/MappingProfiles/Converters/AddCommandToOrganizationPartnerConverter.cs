using AutoMapper;
using Mladim.Application.Features.Members.Partners.Commands.AddPartner;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.MappingProfiles.Resolvers;

public class AddCommandToOrganizationPartnerConverter : ITypeConverter<AddPartnerCommand, OrganizationPartner>
{
    public OrganizationPartner Convert(AddPartnerCommand source, OrganizationPartner destination, ResolutionContext context) =>
        new OrganizationPartner
        {
            Partner = context.Mapper.Map<Partner>(source),
        };
   
}
