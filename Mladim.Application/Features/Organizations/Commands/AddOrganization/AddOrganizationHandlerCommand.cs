using AutoMapper;
using MediatR;
using Mladim.Domain.Models;
using Mladim.Application.Contracts;

namespace Mladim.Application.Features.Organizations.Commands.AddOrganization;

public class AddOrganizationHandlerCommand : IRequestHandler<AddOrganizationCommand, OrganizationDto>
{
    private IUnitOfWork UnitOfWork { get; }
    private IMapper Mapper { get; }
    public AddOrganizationHandlerCommand(IUnitOfWork unitOfWork, IMapper mapper)
    {
        Mapper = mapper;
        UnitOfWork = unitOfWork;       
    }
    public async Task<OrganizationDto> Handle(AddOrganizationCommand request, CancellationToken cancellationToken)
    {
        var organization = Mapper.Map<Organization>(request);

        var response = await UnitOfWork.GetRepository<Organization>()
            .AddAsync(organization);

        return Mapper.Map<OrganizationDto>(response);
    }
}
