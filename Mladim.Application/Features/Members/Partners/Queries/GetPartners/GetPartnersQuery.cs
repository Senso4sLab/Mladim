using MediatR;
using Mladim.Domain.Contracts;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.Partners.Queries.GetPartners;

public class GetPartnersQuery : IRequest<IEnumerable<INameableEntity>>
{
    public int? ProjectId { get; set; }
    public int? ActivityId { get; set; }
    public int? OrganizationId { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsMemberAbbreviated { get; set; }
}
