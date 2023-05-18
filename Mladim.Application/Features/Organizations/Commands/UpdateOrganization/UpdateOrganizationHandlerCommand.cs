using AutoMapper;
using MediatR;
using Mladim.Domain.Models;
using Mladim.Application.Contracts;
using Mladim.Application.Features.Organizations.Commands.AddOrganization;

namespace Mladim.Application.Features.Organizations.Commands.UpdateOrganization;

public class UpdateOrganizationHandlerCommand : IRequestHandler<UpdateOrganizationCommand, int>
{
    private IMapper Mapper { get; }
    private IUnitOfWork UnitOfWork { get; }    
    public UpdateOrganizationHandlerCommand(IUnitOfWork unitOfWork, IMapper mapper)
    {
        Mapper = mapper;
        UnitOfWork = unitOfWork;        
    }
    public async Task<int> Handle(UpdateOrganizationCommand request, CancellationToken cancellationToken)
    {
        var organization = Mapper.Map<Organization>(request);

        this.UnitOfWork.OrganizationRepository.Update(organization);

        return await this.UnitOfWork.SaveChangesAsync();
    }
}
