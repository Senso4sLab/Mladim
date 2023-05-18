using FluentValidation;
using MediatR.NotificationPublishers;
using Mladim.Application.Contracts;
using Mladim.Application.Features.Members.Partners.Commands.AddPartner;
using Mladim.Application.Features.Members.StaffMembers.Commands.AddStaffMember;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Validators;

public class AddPartnerCommandValidator : AbstractValidator<AddPartnerCommand>
{
    public IUnitOfWork UnitOfWork { get; }

    public AddPartnerCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(c => c.OrganizationId)
            .NotNull()
            .WithMessage("{PropertyName} je zahtevan");

        RuleFor(c => c.OrganizationId)
            .MustAsync(ExistOrganization)
            .When(WhenOrganizationIsNotNull)
            .WithMessage("Organizacija ne obstaja");

        UnitOfWork = unitOfWork;
    }

    private bool WhenOrganizationIsNotNull(AddPartnerCommand command)
    {
        return command.OrganizationId != null;
    }

    private Task<bool> ExistOrganization(int? organizationId, CancellationToken token)
    {
        return this.UnitOfWork.GetRepository<Organization>().AnyAsync(o => o.Id == organizationId);
    }

}
