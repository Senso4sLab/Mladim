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

public class AddPartnerCommandHandler : IRequestHandler<AddPartnerCommand, PartnerQueryDetailsDto>
{
    public IUnitOfWork UnitOfWork { get; }
    public IMapper Mapper { get; }
    public AddPartnerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }
    public async Task<PartnerQueryDetailsDto> Handle(AddPartnerCommand request, CancellationToken cancellationToken)
    {
        if (request.OrganizationId == null)
            throw new Exception("Organizacija ne obstaja");

        var organization = await this.UnitOfWork.OrganizationRepository
            .FirstOrDefaultAsync(o => o.Id == request.OrganizationId);
        
        if(organization == null)
            throw new Exception("Organizacija ne obstaja");
        
        var orgPartner = this.Mapper.Map<OrganizationPartner>(request);

        organization.Partners.Add(orgPartner);

        await this.UnitOfWork.SaveChangesAsync();

        return this.Mapper.Map<PartnerQueryDetailsDto>(orgPartner.Partner);
    }
}
