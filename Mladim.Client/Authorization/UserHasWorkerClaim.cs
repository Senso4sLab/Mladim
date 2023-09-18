using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Mladim.Client.Models;

namespace Mladim.Client.Authorization;

public class UserHasWorkerClaim : IAuthorizationRequirement
{   
}
