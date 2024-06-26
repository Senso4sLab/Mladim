﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contracts.File;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Projects.Commands.UpdateProject;

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, int>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }

    public IFileApiService FileApiService { get; }  

    public UpdateProjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileApiService apiService) =>
       (UnitOfWork, Mapper, FileApiService) = (unitOfWork, mapper, apiService);

    public async Task<int> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await this.UnitOfWork.ProjectRepository
            .GetProjectDetailsAsync(request.Id);

        ArgumentNullException.ThrowIfNull(project);

        project = this.Mapper.Map(request, project);

        var partner = this.Mapper.Map<IEnumerable<Partner>>(request.Partners);
        project.Partners.RemoveAll(p => !partner.Any(rp => rp.Equals(p)));

        var addPartner = partner.Where(rp => !project.Partners.Any(p => p.Equals(rp)));
        this.UnitOfWork.ConfigEntitiesState(EntityState.Unchanged, addPartner);
        project.Partners.AddRange(addPartner);

        var group = this.Mapper.Map<IEnumerable<ProjectGroup>>(request.Groups);
        project.Groups.RemoveAll(g => !group.Any(rp => rp.Equals(g)));

        var addGroup = group.Where(rg => !project.Groups.Any(g => g.Equals(rg)));
        this.UnitOfWork.ConfigEntitiesState(EntityState.Unchanged, addPartner);
        project.Groups.AddRange(addGroup);

        // files

        var removedFiles = project.Files.Where(f => !request.Files.Any(rf => rf.FileName == f.FileName)).ToList();
        
        foreach(var file in removedFiles)
        {
            var fileUrl = Path.Combine("Files",file.FolderName, file.StoredFileName);
            FileApiService.DeleteFile(fileUrl);
            project.Files.Remove(file);
        }     

        var addedFiles = request.Files.Where(rf => !project.Files.Any(f => f.FileName == rf.FileName)).ToList();
        
        foreach(var file in addedFiles)
        {
            string trustedFileName = await FileApiService.AddFileAsync(file.Data.ToArray(), "Projects", file.FileName);
            project.Files.Add(AttachedFile.Create(file.FileName, trustedFileName, file.ContentType, "Projects"));
        }     

        return await this.UnitOfWork.SaveChangesAsync();      
    }
}
