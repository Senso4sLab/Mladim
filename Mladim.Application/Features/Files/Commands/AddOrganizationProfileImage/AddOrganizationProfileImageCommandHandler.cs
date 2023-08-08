using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.File;
using Mladim.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Files.Commands.AddOrganizationProfileImage;

public class AddOrganizationProfileImageCommandHandler : IRequestHandler<AddOrganizationImageProfileCommand, string>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }
    public IFileApiService FileApiService { get; set; }  

    public AddOrganizationProfileImageCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileApiService apiService) =>
        (UnitOfWork, Mapper, FileApiService) = (unitOfWork, mapper, apiService);
  
    public async Task<string> Handle(AddOrganizationImageProfileCommand request, CancellationToken cancellationToken)
    {
        var organization = await this.UnitOfWork.OrganizationRepository.FirstOrDefaultAsync(o => o.Id == request.OrganizationId);
        ArgumentNullException.ThrowIfNull(organization);

        string folderPath = Path.Combine("Images", "OrganizationProfiles");

        if (organization.Attributes.LogoUrl != null)
            this.FileApiService.DeleteFile(organization.Attributes.LogoUrl, folderPath);

        string trustedFileName = await this.FileApiService.AddFileAsync(request.Data.ToArray(), Path.Combine("Images", "OrganizationProfiles"), request.FileName);

        organization.Attributes.LogoUrl = $"Files\\{folderPath}\\{trustedFileName}";

        await this.UnitOfWork.SaveChangesAsync();

        return organization.Attributes.LogoUrl;

    }
}
