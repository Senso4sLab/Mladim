using AutoMapper;
using MediatR;
using Mladim.Domain.Models;
using Mladim.Application.Contracts;
using Mladim.Domain.IdentityModels;

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
            var appUser = await UnitOfWork.GetRepository<ApplicationUser>()
                .GetFirstOrDefaultAsync(au => au.Id == request.AppUserId);

            if (appUser == null)
                throw new Exception("");

            organization.AppUsers.Add(appUser);
        }        

        var response = await UnitOfWork.GetRepository<Organization>()
               .AddAsync(organization);

        await this.UnitOfWork.SaveChangesAsync();

        return Mapper.Map<OrganizationDto>(response);
    }
}
