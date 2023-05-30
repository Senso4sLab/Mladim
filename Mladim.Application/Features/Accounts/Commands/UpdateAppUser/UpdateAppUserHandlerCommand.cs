using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Accounts.Commands.UpdateAppUser;

public class UpdateAppUserHandlerCommand : IRequestHandler<UpdateAppUserCommand, bool>
{

    private IMapper Mapper { get; }
    private IUnitOfWork UnitOfWork { get; }

    private UserManager<AppUser> UserManager { get; }
    public UpdateAppUserHandlerCommand(UserManager<AppUser> userManager,  IUnitOfWork unitOfWork, IMapper mapper)
    {
        Mapper = mapper;
        UnitOfWork = unitOfWork;
        UserManager = userManager;   
    }
   
    public async Task<bool> Handle(UpdateAppUserCommand request, CancellationToken cancellationToken)
    {
        var appUser = await this.UserManager.FindByIdAsync(request.Id);

        appUser = this.Mapper.Map(request, appUser);

        var identityResult  = await this.UserManager.UpdateAsync(appUser);

        return identityResult.Succeeded;      
    }
}
