﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Dtos;
using Mladim.Domain.Enums;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Groups.Commands.AddGroup;

public class AddOrganizationGroupCommandHandler : IRequestHandler<AddGroupCommand, GroupQueryDto>
{
    public IMapper Mapper { get; }

    public IUnitOfWork UnitOfWork { get; }   

    public AddOrganizationGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.Mapper = mapper;
        this.UnitOfWork = unitOfWork;        
    }
    public async Task<GroupQueryDto> Handle(AddGroupCommand request, CancellationToken cancellationToken)
    {
        var organization = await this.UnitOfWork.OrganizationRepository.FirstOrDefaultAsync(o => o.Id == request.OrganizationId);

        ArgumentNullException.ThrowIfNull(organization);

        var group = Group.Create(request.GroupType, request.FullName, request.Description, request.Members, request.OrganizationId);        

        this.UnitOfWork.ConfigEntitiesState(EntityState.Unchanged, group.Members);
        
        group = await this.UnitOfWork.GroupRepository.AddAsync(group);

        await this.UnitOfWork.SaveChangesAsync();      

        return this.Mapper.Map<GroupQueryDto>(group);
    }

  
    
}
