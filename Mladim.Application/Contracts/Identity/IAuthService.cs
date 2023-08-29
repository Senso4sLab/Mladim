
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
    Task<bool> AddClaimAsync(AppUser user, Claim newClaim);
    Task<bool> ExistClaimValueAsync(AppUser user, string claimValue);
    Task<Result<AuthResponse>> LoginAsync(string email, string password);
    Task<Result<RegistrationResponse>> RegisterAsync(string name, string surname, string nickname, string email, string? password = null);
    Task<bool> ReplaceClaimAsync(AppUser user, Claim newClaim);
    
}
