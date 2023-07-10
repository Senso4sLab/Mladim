using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.Partners.Commands.AddPartner;

public class AddPartnerCommandHandler : IRequestHandler<AddPartnerCommand, bool>
{
    public IUnitOfWork UnitOfWork { get; }
    public IMapper Mapper { get; }
    public AddPartnerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }
    public async Task<bool> Handle(AddPartnerCommand request, CancellationToken cancellationToken)
    {     

        var organization = await this.UnitOfWork.OrganizationRepository
            .FirstOrDefaultAsync(o => o.Id == request.OrganizationId);

        ArgumentNullException.ThrowIfNull(organization);

        var organizationPartner = this.Mapper.Map<OrganizationPartner>(request);

        organization.Partners.Add(organizationPartner);

        return await this.UnitOfWork.SaveChangesAsync() > 0;    
    }
}
