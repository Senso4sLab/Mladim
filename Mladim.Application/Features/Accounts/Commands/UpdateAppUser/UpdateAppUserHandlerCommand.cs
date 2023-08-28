using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Accounts.Commands.UpdateAppUser;

public class UpdateAppUserHandlerCommand : IRequestHandler<UpdateAppUserCommand, int>
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
   
    public async Task<int> Handle(UpdateAppUserCommand request, CancellationToken cancellationToken)
    {
        var appUser = await this.UserManager.FindByIdAsync(request.Id);

        ArgumentNullException.ThrowIfNull(appUser);

        var appUserEmail = appUser.Email!;       
        
        appUser = this.Mapper.Map(request, appUser);

        var identityResult  = await this.UserManager.UpdateAsync(appUser);

        if (!identityResult.Succeeded)
            return 0;

        if (appUserEmail != request.Email)
        {
            var response = await UpdateStaffMembersEmailAsync(appUserEmail, request.Email);
            return response ? 1: 0; 
        }
        return identityResult.Succeeded ? 1 : 0;    
    }

    private async Task<bool> UpdateStaffMembersEmailAsync(string oldEmail, string updatedEmail)
    {
       var staffMembers = await this.UnitOfWork.StaffMemberRepository
            .GetAllAsync(sm => sm.Email == oldEmail);       

       foreach(var staffMember in staffMembers)
       { 
          staffMember.Email = updatedEmail;
       }

       return await this.UnitOfWork.SaveChangesAsync() > 0 || !staffMembers.Any();
    }
}
