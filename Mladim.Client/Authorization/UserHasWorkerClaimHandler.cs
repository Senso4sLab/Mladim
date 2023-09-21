using Microsoft.AspNetCore.Authorization;
using Mladim.Domain.Enums;

namespace Mladim.Client.Authorization;

public class UserHasWorkerClaimHandler : AuthorizationHandler<UserHasWorkerClaim, string>
{  


    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UserHasWorkerClaim requirement, string orgId)
    {
       var existManagerClaim = context.User.Claims
               .Any(c => c.Type == nameof(ApplicationClaim.Worker) && c.Value == orgId);              

        if (existManagerClaim)
            context.Succeed(requirement);
        else
            context.Fail(); 
       
    }
}
