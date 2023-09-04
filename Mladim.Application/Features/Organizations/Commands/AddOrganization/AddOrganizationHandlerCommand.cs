using AutoMapper;
using MediatR;
using Mladim.Domain.Models;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Dtos;
using Mladim.Application.Contracts.Persistence;
using Mladim.Application.Contracts.File;


namespace Mladim.Application.Features.Organizations.Commands.AddOrganization;

public class AddOrganizationHandlerCommand : IRequestHandler<AddOrganizationCommand, OrganizationQueryDto>
{
    private IMapper Mapper { get; }
    private IUnitOfWork UnitOfWork { get; }

    private IFileApiService FileService { get; } 
    
    
    public AddOrganizationHandlerCommand(IFileApiService fileService, IUnitOfWork unitOfWork, IMapper mapper)
    {
        FileService = fileService;
        UnitOfWork = unitOfWork;
        Mapper = mapper;
         
    }
    public async Task<OrganizationQueryDto> Handle(AddOrganizationCommand request, CancellationToken cancellationToken)
    {
        var organization = Mapper.Map<Organization>(request);        
        
        organization = await UnitOfWork.OrganizationRepository.AddAsync(organization);

        if (request.AppUserId is string appUserId)
        {
            var appUser = await UnitOfWork.AppUserRepository.FindByIdAsync(appUserId);             

            ArgumentNullException.ThrowIfNull(appUser);

            appUser.Organizations.Add(organization);
        }

        //if (request.BannerImage is ImageData bannerImg)
        //{
        //    string folderPath = Path.Combine("Images", "OrganizationBanners");

        //    string trustedFileName = await this.FileService.AddFileAsync(bannerImg.Data.ToArray(), folderPath, bannerImg.FileName);

        //    organization.Attributes.BannerUrl = $"Files\\{folderPath}\\{trustedFileName}";
        //}

        //if (request.LogoImage is ImageData logoImg)
        //{
        //    string folderPath = Path.Combine("Images", "OrganizationProfiles");

        //    string trustedFileName = await this.FileService.AddFileAsync(logoImg.Data.ToArray(), folderPath, logoImg.FileName);

        //    organization.Attributes.BannerUrl = $"Files\\{folderPath}\\{trustedFileName}";
        //}

        await this.UnitOfWork.SaveChangesAsync();

        return this.Mapper.Map<OrganizationQueryDto>(organization);
    }
}
