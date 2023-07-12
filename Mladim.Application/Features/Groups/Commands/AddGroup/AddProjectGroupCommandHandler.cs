using AutoMapper;
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

public class AddProjectGroupCommandHandler : IRequestHandler<AddProjectGroupCommand, bool>
{
    public IUnitOfWork UnitOfWork { get; }
    public IMapper Mapper { get; }


    public AddProjectGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.UnitOfWork = unitOfWork;
        this.Mapper = mapper;
    }

    public async Task<bool> Handle(AddProjectGroupCommand request, CancellationToken cancellationToken)
    {
        var project = await this.UnitOfWork.ProjectRepository.FirstOrDefaultAsync(o => o.Id == request.ProjectId);

        ArgumentNullException.ThrowIfNull(project);

        var group = await this.UnitOfWork.GroupRepository.FirstOrDefaultAsync(o => o.Id == request.GroupId) as ProjectGroup;

        ArgumentNullException.ThrowIfNull(group);     

        project.Add(group);        

        return await this.UnitOfWork.SaveChangesAsync() > 0; 

    }
}
