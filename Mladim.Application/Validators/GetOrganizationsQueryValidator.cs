using FluentValidation;
using Mladim.Application.Contracts.Persistence;
using Mladim.Application.Features.Organizations.Queries.GetOrganizations;
using Mladim.Domain.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Validators;

public class GetOrganizationsQueryValidator : AbstractValidator<GetOrganizationsQuery>
{
    private IUnitOfWork UnitOfWork { get; }
    public GetOrganizationsQueryValidator(IUnitOfWork unitOfWork)
	{
		RuleFor(q => q.AppUserId)			
			.NotEmpty()
			.WithMessage("{PropertyName} je zahtevan");
			
		RuleFor(q => q.AppUserId)			
			.Empty()
			.MustAsync(ExistUserId);			

        this.UnitOfWork = unitOfWork;
    }    

    private async Task<bool> ExistUserId(string appUserId, CancellationToken cancellationToken) =>	
		await this.UnitOfWork.AppUserRepository.ExistUserAsync(appUserId);	
	
}
