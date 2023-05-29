using FluentValidation;
using Mladim.Application.Contracts.Persistence;
using Mladim.Application.Features.Activities.Queries.GetActivities;
using Mladim.Application.Features.Organizations.Queries.GetOrganizations;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Validators;

public class GetActivitiesByProjectQueryValidator : AbstractValidator<GetActivitiesByProjectQuery>
{
    private IUnitOfWork UnitOfWork { get; }
    public GetActivitiesByProjectQueryValidator(IUnitOfWork unitOfWork)
	{	
			
		//RuleFor(q => q.ProjectId)			
		//	.Empty()
		//	.MustAsync(ExistProjectId);			

        this.UnitOfWork = unitOfWork;
    }    

    private async Task<bool> ExistProjectId(int? projectId, CancellationToken cancellationToken) =>	
		await this.UnitOfWork.ProjectRepository.AnyAsync(p => p.Id == projectId);	
	
}
