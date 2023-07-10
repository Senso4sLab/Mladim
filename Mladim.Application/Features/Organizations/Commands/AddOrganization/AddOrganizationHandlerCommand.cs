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
        
        organization = await UnitOfWork.OrganizationRepository.AddAsync(organization);

        if (request.AppUserId is string appUserId)
        {
            var appUser = await UnitOfWork.AppUserRepository.FirstOrDefaultAsync(ap => ap.Id == appUserId);             

            ArgumentNullException.ThrowIfNull(appUser);

            appUser.Organizations.Add(organization);
        }       

        await this.UnitOfWork.SaveChangesAsync();

        return Mapper.Map<OrganizationQueryDto>(organization);     
    }
}
