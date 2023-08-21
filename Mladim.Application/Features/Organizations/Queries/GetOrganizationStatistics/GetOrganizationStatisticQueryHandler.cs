using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Localization;
using Mladim.Application.Contracts.Persistence;
using Mladim.Application.Features.Organizations.Queries.GetOrganizations;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.Members;
using Mladim.Domain.Dtos.Organization;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Organizations.Queries.GetOrganizationStatistics;

public class GetOrganizationStatisticQueryHandler : IRequestHandler<GetOrganizationStatisticQuery, OrganizationStatisticQueryDto>
{
    public IUnitOfWork UnitOfWork { get; }
    public IMapper Mapper { get; set; }
    public GetOrganizationStatisticQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.Mapper = mapper;
        this.UnitOfWork = unitOfWork;
    }
    public async Task<OrganizationStatisticQueryDto> Handle(GetOrganizationStatisticQuery request, CancellationToken cancellationToken)
    {
        var organization  = await this.UnitOfWork.OrganizationRepository.FirstOrDefaultAsync(o => o.Id == request.OrganizationId);
       
        ArgumentNullException.ThrowIfNull(organization);

        if (organization.Attributes.CreatedStamp.Year > request.Year)
            return OrganizationStatisticQueryDto.Empty;

        var currentDate = DateTime.UtcNow;
        
        // Projekti
        var projects = await this.UnitOfWork.ProjectRepository
            .GetAllAsync(p => p.OrganizationId == request.OrganizationId && p.TimeRange.IsSameYearAs(request.Year));

        var activeProjects = projects.Where(p => p.TimeRange.IsDateTimeInRange(currentDate))
            .Select(p => NamedEntityDto.Create(p.Id, p.Attributes.Name))
            .ToList();

        var pastProjects = projects.Where(p => !p.TimeRange.IsDateTimeInRange(currentDate))
            .Select(p => NamedEntityDto.Create(p.Id, p.Attributes.Name))
            .ToList();

        // Aktivnosti

        var activities = await this.UnitOfWork.ActivityRepository
          .GetActivitiesWithParticipantsAsync(a => a.Project.OrganizationId == request.OrganizationId && a.TimeRange.IsSameYearAs(request.Year));

        var activeActivities = activities.Where(a => a.TimeRange.IsDateTimeInRange(currentDate))
            .Select(a => NamedEntityDto.Create(a.Id, a.Attributes.Name))
            .ToList();

        var pastActivites = activities.Where(a => !a.TimeRange.IsDateTimeInRange(currentDate))
            .Select(a => NamedEntityDto.Create(a.Id, a.Attributes.Name))
            .ToList();

        // št. participantov
       
        int anonymousParticipants =  activities.Sum(a => a.AnonymousParticipantGroups.Sum(apg => apg.Number));
       
        int individualParticipants = activities.Sum(a => a.Participants.Count);            

        individualParticipants += activities.Sum(a => a.Groups.Sum(g => g.Members.Count));

        return OrganizationStatisticQueryDto.Create(activeProjects, pastProjects, activeActivities, pastActivites, individualParticipants, anonymousParticipants);
    }
}
