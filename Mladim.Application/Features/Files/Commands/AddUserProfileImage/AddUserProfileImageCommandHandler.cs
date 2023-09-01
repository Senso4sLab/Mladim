using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.File;
using Mladim.Application.Contracts.Persistence;

namespace Mladim.Application.Features.Files.Commands.AddUserProfileImage;

public class AddUserProfileImageCommandHandler : IRequestHandler<AddUserProfileImageCommand, string>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }
    public IFileApiService FileApiService { get; set; }

    public AddUserProfileImageCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileApiService apiService) =>
        (UnitOfWork, Mapper, FileApiService) = (unitOfWork, mapper, apiService);

    public async Task<string> Handle(AddUserProfileImageCommand request, CancellationToken cancellationToken)
    {
        var appUser = await this.UnitOfWork.AppUserRepository.FindByIdAsync(request.UserId);
               
        ArgumentNullException.ThrowIfNull(appUser);

        string folderPath = Path.Combine("Images", "UserProfiles");

        if (appUser.ImageUrl != null)
            this.FileApiService.DeleteFile(appUser.ImageUrl, folderPath);

        string trustedFileName = await this.FileApiService.AddFileAsync(request.Data.ToArray(), Path.Combine("Images", "UserProfiles"), request.FileName);

        appUser.ImageUrl = $"Files\\{folderPath}\\{trustedFileName}";

        await this.UnitOfWork.SaveChangesAsync();

        return appUser.ImageUrl;
    }
}
