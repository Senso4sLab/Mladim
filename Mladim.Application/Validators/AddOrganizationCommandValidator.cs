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
	public AddOrganizationCommandValidator()
	{       
        RuleFor(c => c.Attributes.Name)
            .NotEmpty()
            .WithMessage("{PropertyName} je zahtevan.");
        
        RuleFor(c => c.Attributes.Description)
            .NotEmpty()
            .WithMessage("{PropertyName} je zahtevan.");
    }    
}
