using FluentValidation;
using Mladim.Application.Contracts.Persistence;
using Mladim.Application.Features.Projects.Queries.GetProjects;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Validators;

public class GetProjectsByOrganizationQueryValidator : AbstractValidator<GetProjectsByOrganizationQuery>
{
    public IUnitOfWork UnitOfWork { get; }
    public GetProjectsByOrganizationQueryValidator(IUnitOfWork unitOfWork)
	{
		RuleFor(q => q.OrganizationId)
			.MustAsync(ExistOrganization);
		
		UnitOfWork = unitOfWork;
    }

	private async Task<bool> ExistOrganization(int orgId, CancellationToken token)
	{
		return await this.UnitOfWork.OrganizationRepository.AnyAsync(o => o.Id == orgId);
	}

   
}
