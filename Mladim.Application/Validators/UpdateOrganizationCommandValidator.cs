using FluentValidation;
using Mladim.Application.Contracts;
using Mladim.Application.Features.Organizations.Commands.AddOrganization;
using Mladim.Application.Features.Organizations.Commands.UpdateOrganization;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Validators;

public class UpdateOrganizationCommandValidator : AbstractValidator<UpdateOrganizationCommand>
{
    private IUnitOfWork UnitOfWork { get; }

    public UpdateOrganizationCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(c => c.Id)
           .NotNull()
           .WithMessage("{PropertyName} je zahtevan.");

        RuleFor(c => c.Id)
            .Null()
            .MustAsync(ExistOrganization);

        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("{PropertyName} je zahtevan.");
        
        RuleFor(c => c.Description)
            .NotEmpty()
            .WithMessage("{PropertyName} je zahtevan.");

        UnitOfWork = unitOfWork;
    }
	
    
    private async Task<bool> ExistOrganization(int organizationId, CancellationToken cancellationToken)
    {
        return await this.UnitOfWork.GetRepository<Organization>()
            .AnyAsync(o => o.Id == organizationId);
    }
}
