using AutoMapper;
using MediatR;
using Mladim.Application.Contracts;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.Partners.Commands.AddPartner;

public class AddPartnerCommandHandler : IRequestHandler<AddPartnerCommand, PartnerDto>
{
    public IUnitOfWork UnitOfWork { get; }
    public IMapper Mapper { get; }
    public AddPartnerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }
    

    public async Task<PartnerDto> Handle(AddPartnerCommand request, CancellationToken cancellationToken)
    {
        var partner = this.Mapper.Map<Partner>(request);

        await this.UnitOfWork.GetRepository<Partner>().AddAsync(partner);

        await this.UnitOfWork.SaveChangesAsync();

        return this.Mapper.Map<PartnerDto>(partner);
    }
}
