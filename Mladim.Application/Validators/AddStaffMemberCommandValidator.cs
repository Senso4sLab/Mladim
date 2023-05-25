﻿using FluentValidation;
using MediatR.NotificationPublishers;
using Mladim.Application.Contracts.Persistence;
using Mladim.Application.Features.Members.StaffMembers.Commands.AddStaffMember;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Validators;

public class AddStaffMemberCommandValidator : AbstractValidator<AddStaffMemberCommand>
{
    public IUnitOfWork UnitOfWork { get; }

    public AddStaffMemberCommandValidator(IUnitOfWork unitOfWork)
    {     

        RuleFor(c => c.OrganizationId)
            .MustAsync(ExistOrganization)            
            .WithMessage("Organizacija ne obstaja");

        UnitOfWork = unitOfWork;
    }    

    private Task<bool> ExistOrganization(int organizationId, CancellationToken token)
    {
        return this.UnitOfWork.OrganizationRepository.AnyAsync(o => o.Id == organizationId);
    }

}
