using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Models;
using Mladim.Infrastracture.Persistance;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Mladim.Infrastracture.Repositories;

public class AppUserRepository : GenericRepository<AppUser>, IAppUserRepository
{
    private UserManager<AppUser> UserManager { get; }
    public AppUserRepository(UserManager<AppUser> userManager, ApplicationDbContext context) : base(context)
    {
        this.UserManager = userManager;
    }  

    public async Task<Result<AppUser>> AddAsync(AppUser user, string password)
    {       
        var result = await this.UserManager.CreateAsync(user, password);        

        if (!result.Succeeded)
            return Result<AppUser>.Error(string.Join(", ", result.Errors.Select(e => e.Description)));

        return Result<AppUser>.Success(user);
    }

    public async Task<AppUser?> FindByIdAsync(string userId)
    {
        return await this.UserManager.FindByIdAsync(userId);  
    }

    public async Task<AppUser?> FindByEmailAsync(string email)
    {
        return await this.UserManager.FindByEmailAsync(email);
    }
    public async Task<bool> IsUserInOrganizationAsync(string userId, int organizationId)
    {
        return await this.DbSet.Where(u => u.Id == userId)
            .Include(u => u.Organizations)
            .AnyAsync(u => u.Organizations.Any(o => o.Id == organizationId));
    }



    public async Task<string> ChangePasswordAsync(string userId, string password)
    {
        var user = await this.UserManager.FindByIdAsync(userId);

        ArgumentNullException.ThrowIfNull(user);

        if (!await this.UserManager.CheckPasswordAsync(user, password))
            throw new Exception("Uporabniški podatki niso pravilni.");

        var token = await UserManager.GeneratePasswordResetTokenAsync(user);

        var result = await UserManager.ResetPasswordAsync(user, token, password);

        if (!result.Succeeded)
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

        return user.Id;
    }
}

