using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using CustomAuthorizationTask.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CustomAuthorizationTask.Handler
{
    public class RolesInDBAuthorizationHandler : AuthorizationHandler<RolesAuthorizationRequirement>, IAuthorizationHandler
    {
        private readonly ApplicationDbContext _dbContext;


        public RolesInDBAuthorizationHandler(ApplicationDbContext identityTenantDbContext)
        {
            _dbContext = identityTenantDbContext;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       RolesAuthorizationRequirement requirement)
        {
            if (context.User == null || !context.User.Identity.IsAuthenticated)
            {
                context.Fail();
                return;
            }

            var found = false;
            if (requirement.AllowedRoles == null ||
                requirement.AllowedRoles.Any() == false)
            {
                // it means any logged in user is allowed to access the resource
                found = true;
            }
            else
            {

                var user = context.User.FindFirstValue(ClaimTypes.Email);
                var roles = requirement.AllowedRoles;
                var roleIds = await _dbContext.Roles
                                                    .Where(p => roles.Contains(p.Name))
                                                    .Select(p => p.Id)
                                                    .ToListAsync();

                found = await _dbContext.UserRoles
                                        .Where(p => roleIds.Contains(p.RoleId) || p.UserId == user)
                                        .AnyAsync();
            }

            if (found)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}