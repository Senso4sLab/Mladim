using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.File;
using Mladim.Application.Contracts.Persistence;

namespace Mladim.Application.Features.Files.Commands.DeleteFile;

public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, bool>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }
    public IFileApiService FileApiService { get; set; }

    public DeleteFileCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileApiService apiService) =>
        (UnitOfWork, Mapper, FileApiService) = (unitOfWork, mapper, apiService);
    public async Task<bool> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
    {
        return await Task.FromResult( this.FileApiService.DeleteFile(request.FullFilePath));
    }
}
