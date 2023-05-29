using AutoMapper;
using MediatR;
using Mladim.Domain.Models;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Dtos;
using Mladim.Application.Contracts.Persistence;

namespace Mladim.Application.Features.Organizations.Commands.AddOrganization;

public class AddOrganizationHandlerCommand : IRequestHandler<AddOrganizationCommand, OrganizationQueryDto>
{
    private IMapper Mapper { get; }
    private IUnitOfWork UnitOfWork { get; }
    
    public AddOrganizationHandlerCommand(IUnitOfWork unitOfWork, IMapper mapper)
    {
        Mapper = mapper;
        UnitOfWork = unitOfWork;       
    }
    public async Task<OrganizationQueryDto> Handle(AddOrganizationCommand request, CancellationToken cancellationToken)
    {        
        var organization = Mapper.Map<Organization>(request);

        if (request.AppUserId != null)
        {
            var appUser = await UnitOfWork.AppUserRepository.FirstOrDefaultAsync(ap => ap.Id == request.AppUserId);                            

            if (appUser == null)
                throw new Exception("");

            organization.AppUsers.Add(appUser);
        }        

        organization = await UnitOfWork.OrganizationRepository
               .AddAsync(organization);

        await this.UnitOfWork.SaveChangesAsync();

        return Mapper.Map<OrganizationQueryDto>(organization);
    }
}
