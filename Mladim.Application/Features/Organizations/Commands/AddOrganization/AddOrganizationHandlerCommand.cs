using AutoMapper;
using MediatR;
using Mladim.Domain.Models;
using Mladim.Application.Contracts;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Dtos;

namespace Mladim.Application.Features.Organizations.Commands.AddOrganization;

public class AddOrganizationHandlerCommand : IRequestHandler<AddOrganizationCommand, OrganizationDto>
{
    private IMapper Mapper { get; }
    private IUnitOfWork UnitOfWork { get; }
    
    public AddOrganizationHandlerCommand(IUnitOfWork unitOfWork, IMapper mapper)
    {
        Mapper = mapper;
        UnitOfWork = unitOfWork;       
    }
    public async Task<OrganizationDto> Handle(AddOrganizationCommand request, CancellationToken cancellationToken)
    {        
        var organization = Mapper.Map<Organization>(request);

        if (request.AppUserId != null)
        {
            var applicationUser = await UnitOfWork.MemberRepository
                .GetMemberById<AppUser>(request.AppUserId);               

            if (applicationUser == null)
                throw new Exception("");

            organization.AppUsers.Add(applicationUser);
        }        

        organization = await UnitOfWork.OrganizationRepository
               .AddAsync(organization);

        await this.UnitOfWork.SaveChangesAsync();

        return Mapper.Map<OrganizationDto>(organization);
    }
}
