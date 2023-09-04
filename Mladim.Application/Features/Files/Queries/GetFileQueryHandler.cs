using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Files.Queries;

public class GetFileQueryHandler : IRequestHandler<GetFileQuery, FileResponse?>
{
    public IUnitOfWork UnitOfWork { get; }
    public IMapper Mapper { get; set; }

    public IWebHostEnvironment Env { get; set; }
    public GetFileQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env) =>
        (UnitOfWork, Mapper, Env) = (unitOfWork, mapper, env);
   
    public async Task<FileResponse?> Handle(GetFileQuery request, CancellationToken cancellationToken)
    {
        var file = await FindAttachedFileAsync(request.FileName, request.ActivityId, request.ProjectId);
        
        if (file == null)
            return null;
        
        var path = Path.Combine(this.Env.WebRootPath, "Files", file.FolderName, file.StoredFileName);

        var memory = new MemoryStream();
        using (var strem = new FileStream(path, FileMode.Open))
        {
            await strem.CopyToAsync(memory);
        }
        memory.Position = 0;

        return FileResponse.Create(memory, file.FileName, file.ContentType);     
            
    }

    private async Task<AttachedFile?> FindAttachedFileAsync(string fileName, int? activityId, int? projectId)
    {
        if (string.IsNullOrEmpty(fileName) || (activityId == null && projectId == null))
            return null;

        if (projectId is int pId)
        {
            var project = await this.UnitOfWork.ProjectRepository.FindAsync(pId);
            return project?.Files.FirstOrDefault(af => af.FileName == fileName);
        }

        if (activityId is int aId)
        {
            var activity = await this.UnitOfWork.ActivityRepository.FindAsync(aId);
            return activity?.Files.FirstOrDefault(af => af.FileName == fileName);
        }
        return null;
    }
}
