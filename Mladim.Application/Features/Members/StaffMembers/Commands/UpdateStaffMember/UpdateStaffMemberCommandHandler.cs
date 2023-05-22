using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.StaffMembers.Commands.UpdateStaffMember;

public class UpdatePartnerCommandHandler : IRequestHandler<UpdateStaffMemberCommand, int>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }
    
    public UpdatePartnerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    } 

    public async Task<int> Handle(UpdateStaffMemberCommand request, CancellationToken cancellationToken)
    {
       var staffMember = this.Mapper.Map<StaffMember>(request);    
       this.UnitOfWork.StaffMemberRepository.Update(staffMember);

       return await this.UnitOfWork.SaveChangesAsync();
    }
}
