using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Mladim.Application.Contracts.PowerBIService;
using Mladim.Domain.Dtos.PowerBI;
using System.Numerics;

namespace Mladim.Application.Features.PowerBI.Queries.GetADToken;

public class GetEmbeededReportHandler : IRequestHandler<GetEmbeddedReportQuery, EmbeddedReportDto>
{
    private readonly IPowerBIService _powerBIService;
    private readonly IMapper _mapper;

    public GetEmbeededReportHandler(IPowerBIService powerBIService, IMapper mapper)
    {
        _powerBIService = powerBIService;
        _mapper = mapper;
    }
    public async Task<EmbeddedReportDto> Handle(GetEmbeddedReportQuery request, CancellationToken cancellationToken)
    {
        Guid workspaceId = new Guid("70bc8332-1f7b-4766-969d-717d039927a1");
        Guid reportId =    new Guid("c090bd15-233d-4a4f-9f23-070099d834fa");
        var report = await _powerBIService.GetReport(workspaceId, reportId);        
        return _mapper.Map<EmbeddedReportDto>(report);
    }
}
