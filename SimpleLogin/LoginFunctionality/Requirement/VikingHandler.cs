using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SimpleLogin.LoginFunctionality.Requirement {
public class VikingHandler : AuthorizationHandler<VikingRequirement> {
    
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, VikingRequirement requirement) {
        ClaimsPrincipal claimsPrincipal = context.User;

        if (claimsPrincipal.HasClaim("Role", "Viking")) {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
}