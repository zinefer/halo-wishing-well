using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WishingWell.Api.Application
{
    public class AllowAnonymous : IAuthorizationHandler
    {
        // This authorization handler will bypass all auth requirements and should
        // NEVER be used in PRODUCTION
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            foreach (IAuthorizationRequirement requirement in context.PendingRequirements.ToList())
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}