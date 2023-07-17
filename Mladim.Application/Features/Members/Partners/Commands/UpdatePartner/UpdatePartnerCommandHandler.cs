using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.StaffMembers.Commands.UpdatePartner;

public class UpdatePartnerCommandHandler : IRequestHandler<UpdatePartnerCommand, int>
{
    public IUnitOfWork UnitOfWork { get; }
    public IMapper Mapper { get; }
    public UpdatePartnerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    } 

    public async Task<int> Handle(UpdatePartnerCommand request, CancellationToken cancellationToken)
    {
        var partner = await this.UnitOfWork.PartnerRepository.FirstOrDefaultAsync(p => p.Id == request.Id);

       ArgumentNullException.ThrowIfNull(partner);

       partner = this.Mapper.Map(request, partner); 

       this.UnitOfWork.PartnerRepository.Update(partner);
       return await this.UnitOfWork.SaveChangesAsync();
    }
}
