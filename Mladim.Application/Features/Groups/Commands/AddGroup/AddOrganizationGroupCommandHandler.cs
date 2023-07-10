using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
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

        var group = Group.Create(request.GroupType, request.Name, request.Description, request.Members);

        organization.Add(group);       

        await this.UnitOfWork.SaveChangesAsync();

        return await this.UnitOfWork.SaveChangesAsync() > 0;
    }
}
