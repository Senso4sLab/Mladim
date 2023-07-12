using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Enums;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Groups.Commands.AddGroup;

public class AddOrganizationGroupCommandHandler : IRequestHandler<AddOrganizationGroupCommand, bool>
{
    public IMapper Mapper { get; }

    public IUnitOfWork UnitOfWork { get; }   

    public AddOrganizationGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.Mapper = mapper;
        this.UnitOfWork = unitOfWork;        
    }
    public async Task<bool> Handle(AddOrganizationGroupCommand request, CancellationToken cancellationToken)
    {
        var organization = await this.UnitOfWork.OrganizationRepository.FirstOrDefaultAsync(o => o.Id == request.OrganizationId);

        ArgumentNullException.ThrowIfNull(organization);

        var group = CreateGroup(request.GroupType, request.Name, request.Description, request.Members);
        
        ArgumentNullException.ThrowIfNull(group);

        this.UnitOfWork.ConfigEntityState(EntityState.Unchanged, group.Members);
        
        await this.UnitOfWork.GroupRepository.AddAsync(group);       

        return await this.UnitOfWork.SaveChangesAsync() > 0;
    }

    private Group? CreateGroup(GroupType groupType, string name, string description, IEnumerable<int> memberIds) =>
        groupType switch
        {
            GroupType.Project => Group.Create(MemberType.StaffMember, name, description, memberIds),
            GroupType.Activity => Group.Create(MemberType.Participant, name, description, memberIds),
            _ => null
        };
    
}
