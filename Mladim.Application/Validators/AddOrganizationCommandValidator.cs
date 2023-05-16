using FluentValidation;
using Mladim.Application.Contracts;
using Mladim.Application.Features.Organizations.Commands.AddOrganization;
using Mladim.Domain.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Validators;

public class AddOrganizationCommandValidator : AbstractValidator<AddOrganizationCommand>
{
    private IUnitOfWork UnitOfWork { get; }
    public AddOrganizationCommandValidator(IUnitOfWork unitOfWork)
	{       
        RuleFor(c => c.Name)
            .NotEmpty()           
            .WithMessage("{PropertyName} je zahtevan.");
        
        RuleFor(c => c.Description)
            .NotEmpty()            
            .WithMessage("{PropertyName} je zahtevan.");      

        UnitOfWork = unitOfWork;
    }   

  
}
