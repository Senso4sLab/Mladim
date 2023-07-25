using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mladim.Application.Contracts.File;

namespace Mladim.Application.Features.Projects.Commands.AddProject;

public class AddProjectCommandHandler : IRequestHandler<AddProjectCommand, bool>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }
    public IFileApiService FileApiService { get; set; }

    public AddProjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileApiService apiService) =>
        (UnitOfWork, Mapper, FileApiService) = (unitOfWork, mapper, apiService);
    

    public async Task<bool> Handle(AddProjectCommand request, CancellationToken cancellationToken)
    {
        var organization = await this.UnitOfWork.OrganizationRepository
            .FirstOrDefaultAsync(o => o.Id == request.OrganizationId);

        ArgumentNullException.ThrowIfNull(organization);

        var project = this.Mapper.Map<Project>(request);
        this.UnitOfWork.ConfigEntitiesState(EntityState.Unchanged, project.Groups);
        this.UnitOfWork.ConfigEntitiesState(EntityState.Unchanged, project.Partners);

        // uploaded files
        foreach (var file in request.Files)
        {
            string trustedFileName = await FileApiService.AddFileAsync(file.Data.ToArray(), "Projects", file.FileName);
            project.Files.Add(AttachedFile.Create(file.FileName, trustedFileName, file.ContentType, "Projects"));
        }
        organization.Projects.Add(project);

        var result = await this.UnitOfWork.SaveChangesAsync() > 0;
        return result;       

    }




  
}
