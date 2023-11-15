using CustomAuthorizationTask.Data;
using CustomAuthorizationTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CustomAuthorizationTask.Permissions
{
    public class PermissionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public async Task Invoke(HttpContext context, UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {
            // Get the user's email from the current context
            var userEmail = context.User.FindFirst(ClaimTypes.Email)?.Value;

            // If the email is not found or user is not authenticated, proceed to the next middleware
            if (string.IsNullOrEmpty(userEmail) || !context.User.Identity.IsAuthenticated)
            {
                await _requestDelegate(context);
                return;
            }

            // Retrieve the user from the database based on email
            var user = await userManager.FindByEmailAsync(userEmail);

            // If the user is not found, proceed to the next middleware
            if (user == null)
            {
                await _requestDelegate(context);
                return;
            }

            // Get the roles assigned to the user
            var roles = await userManager.GetRolesAsync(user);

            // Check permissions for each role
            foreach (var role in roles)
            {
                // Retrieve the role ID from the database based on the role name
                var roleId = await dbContext.Roles
                    .Where(r => r.Name == role)
                    .Select(r => r.Id)
                    .FirstOrDefaultAsync();

                if (roleId != null)
                {
                    // Retrieve claims from the database based on role ID
                    var claims = await dbContext.RoleClaims
                        .Where(rc => rc.RoleId == roleId)
                        .Select(rc => rc.ClaimValue)
                        .ToListAsync();

                    // Check if the required claims exist for the current role
                    // Add more conditions for other paths and permissions as needed
                    if (context.Request.Path == "/api/categories" && !claims.Contains(Permissions.Catagory.View))
                    {
                        // If the user does not have the required permission, return a 403 Forbidden response
                        context.Response.StatusCode = 403;
                        return;
                    }
                }
            }

            // If the user has the required permissions, proceed to the next middleware
            await _requestDelegate(context);
        }
    }
}
