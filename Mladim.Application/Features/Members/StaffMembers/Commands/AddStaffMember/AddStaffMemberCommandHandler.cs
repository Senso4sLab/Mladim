using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.StaffMembers.Commands.AddStaffMember;




public class AddStaffMemberCommandHandler : IRequestHandler<AddStaffMemberCommand, StaffMemberDto>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }

    public AddStaffMemberCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }
   
    public async Task<StaffMemberDto> Handle(AddStaffMemberCommand request, CancellationToken cancellationToken)
    {
        var staffMember = this.Mapper.Map<StaffMember>(request);

        await this.UnitOfWork.StaffMemberRepository.AddAsync(staffMember);

        await this.UnitOfWork.SaveChangesAsync();

        return this.Mapper.Map<StaffMemberDto>(staffMember);
    }
}
