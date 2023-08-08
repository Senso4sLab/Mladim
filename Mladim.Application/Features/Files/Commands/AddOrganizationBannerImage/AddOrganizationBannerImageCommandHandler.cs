using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.File;
using Mladim.Application.Contracts.Persistence;
using Mladim.Application.Features.Files.Commands.AddOrganizationProfileImage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Files.Commands.AddOrganizationBannerImage;

public class AddOrganizationBannerImageCommandHandler : IRequestHandler<AddOrganizationImageBannerCommand, string>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }
    public IFileApiService FileApiService { get; set; }

    public AddOrganizationBannerImageCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileApiService apiService) =>
        (UnitOfWork, Mapper, FileApiService) = (unitOfWork, mapper, apiService);

    public async Task<string> Handle(AddOrganizationImageBannerCommand request, CancellationToken cancellationToken)
    {
        var organization = await this.UnitOfWork.OrganizationRepository.FirstOrDefaultAsync(o => o.Id == request.OrganizationId);
        ArgumentNullException.ThrowIfNull(organization);

        string folderPath = Path.Combine("Images", "OrganizationBanners");

        if (organization.Attributes.BannerUrl != null)
            this.FileApiService.DeleteFile(organization.Attributes.BannerUrl, folderPath);

        string trustedFileName = await this.FileApiService.AddFileAsync(request.Data.ToArray(), Path.Combine("Images", "OrganizationBanners"), request.FileName);

        organization.Attributes.BannerUrl = $"Files\\{folderPath}\\{trustedFileName}";

        await this.UnitOfWork.SaveChangesAsync();

        return organization.Attributes.BannerUrl;

    }
}
