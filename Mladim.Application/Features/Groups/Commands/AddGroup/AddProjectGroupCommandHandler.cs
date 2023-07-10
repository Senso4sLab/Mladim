using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Application.Features.Members.StaffMembers.Commands.AddStaffMember;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Groups.Commands.AddGroup;

public class AddProjectGroupCommandHandler : IRequestHandler<AddProjectGroupCommand, GroupQueryDto>
{
    public IUnitOfWork UnitOfWork { get; }

    public AddProjectGroupCommandHandler(IUnitOfWork unitOfWork)
    {
        this.UnitOfWork = unitOfWork;
    }

    public async Task<GroupQueryDto> Handle(AddProjectGroupCommand request, CancellationToken cancellationToken)
    {
        var projectGroup = ProjectGroup.Create(request.Name, request.Description, request.Members.Select(StaffMember.Create));

        var addedGroup = await this.UnitOfWork.GroupRepository.AddAsync(projectGroup);

        await this.UnitOfWork.SaveChangesAsync();

        return GroupQueryDto.Create(addedGroup.Id, addedGroup.Name);

    }
}
