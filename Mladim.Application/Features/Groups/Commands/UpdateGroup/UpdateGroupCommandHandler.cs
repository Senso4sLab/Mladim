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

namespace Mladim.Application.Features.Groups.Commands.UpdateGroup;

public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, int>
{
    public IUnitOfWork UnitOfWork { get; }
    public IMapper Mapper { get; set; }

    public UpdateGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.Mapper = mapper;
        this.UnitOfWork = unitOfWork;
    }
    public async Task<int> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var oldGroup = await this.UnitOfWork.GroupRepository.GetGroupDetailsAsync(request.Id);

        ArgumentNullException.ThrowIfNull(oldGroup);

        oldGroup = this.Mapper.Map(request, oldGroup);

        var rMembers =  request.Members.Select(id => new Member(id)).ToList();

        oldGroup.Members.RemoveAll(m => !rMembers.Any(rm => rm.Equals(m)));

        var addMembers = rMembers.Where(rm => !oldGroup.Members.Any(m => m.Equals(rm))).ToList();
        this.UnitOfWork.ConfigEntitiesState(EntityState.Unchanged, addMembers);
        oldGroup.Members.AddRange(addMembers);
        
        return await this.UnitOfWork.SaveChangesAsync();
    }

    //private Group GetGroupFromUpdateCommand(UpdateGroupCommand request, Group oldGroup) =>
    //    oldGroup switch
    //    {
    //        ProjectGroup => ProjectGroup.Create(GroupType.Project, request.FullName, request.Description, request.Members),
    //        ActivityGroup => ActivityGroup.Create(GroupType.Activity, request.FullName, request.Description, request.Members),
    //        _ => throw new NotImplementedException()
    //    };

    
}
