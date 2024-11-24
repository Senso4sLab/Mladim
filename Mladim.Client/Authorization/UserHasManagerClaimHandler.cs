using Microsoft.AspNetCore.Authorization;
using Mladim.Domain.Enums;
using System.Security.Claims;

namespace Mladim.Client.Authorization;

public class UserHasManagerClaimHandler : AuthorizationHandler<UserHasManagerClaim, string>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UserHasManagerClaim requirement, string orgId)
    {     

        var existManagerClaim = context.User.Claims
                .Any(c => (c.Type == nameof(ApplicationClaim.Manager) || c.Type == ClaimTypes.Role)  && (c.Value == orgId || c.Value == "Admin"));

        if (existManagerClaim)
            context.Succeed(requirement);
        else
            context.Fail();

    }
}
