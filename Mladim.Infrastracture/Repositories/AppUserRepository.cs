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

public class AppUserRepository: IAppUserRepository
{
    private UserManager<AppUser> UserManager { get; }
    public AppUserRepository(UserManager<AppUser> userManager) 
    {
        this.UserManager = userManager;
    }

    public async Task<bool> ExistUserAsync(string userId) =>
        await this.UserManager.Users.AnyAsync(u => u.Id == userId);

    public async Task<Result<AppUser>> CreateAsync(AppUser user, string password)
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
        try
        {
            return await this.UserManager.FindByEmailAsync(email);
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public async Task<Result> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
    {
        var user = await this.UserManager.FindByIdAsync(userId);

        if (user == null)
            return Result.Error("Uporabnik ne obstaja.");

      

        if (!await this.UserManager.CheckPasswordAsync(user, oldPassword))
            return Result.Error("Vnešeni podatki niso pravilni.");

        var token = await UserManager.GeneratePasswordResetTokenAsync(user);

        var result = await UserManager.ResetPasswordAsync(user, token, newPassword);

        if (!result.Succeeded)
            return Result.Error("Gesla ni mogoče spremeniti! Preveri, ali vsebuje vsaj vsaj eno veliko črko, eno malo črko, eno številko in en poseben znak.");

        return Result.Success();
    }
}

