﻿
using Microsoft.AspNetCore.Identity;
using Mladim.Application.Models;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Contracts.Identity;

public interface IAuthService
{
    Task<bool> IsAdminAsync(AppUser user, int organizationId);   
    Task<Result<AuthResponse>> LoginAsync(LoginUser request);
    Task<Result<AuthResponse>> ChangePasswordAsync(string userId, string password);
    Task<Result<RegistrationResponse>> RegisterAsync(UserRegistration request);
    Task<AppUser?> ExistAppUserAsync(string email);
    string GenerateAppUserPassword();
    Task<bool> UpsertClaimAsync(AppUser user, Claim newClaim);
    Task<bool> ExistClaimAsync(AppUser user, string claimValue);
    Task<AppUser> CreateUserWithClaimAsync(string name, string surname, string email, Claim claim);
}
