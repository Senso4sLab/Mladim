using MediatR;
using Mladim.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.Partners.Queries.GetPartner;

public class GetPartnerQuery : IRequest<PartnerDto>
{
    public int PartnerId { get; set; }
}
