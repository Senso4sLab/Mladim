using MediatR;
using Mladim.Application.Features.Organizations.Queries.GetOrganization;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.PowerBI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.PowerBI.Queries.GetADToken;

public class GetEmbeddedReportQuery : IRequest<EmbeddedReportDto>
{
}
