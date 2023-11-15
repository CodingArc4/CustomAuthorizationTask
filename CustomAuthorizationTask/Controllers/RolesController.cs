using CustomAuthorizationTask.Models;
using CustomAuthorizationTask.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CustomAuthorizationTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _role;
        private readonly UserManager<ApplicationUser> _user;

        public RolesController(RoleManager<IdentityRole> role,UserManager<ApplicationUser> user)
        {
            _role = role;
            _user = user;
        }

        //api endpoint to get roles
        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = _role.Roles.ToList();
            var roleViewModels = new List<RolesViewModel>();

            foreach (var role in roles)
            {
                var roleViewModel = new RolesViewModel
                {
                    Id = role.Id,
                    RoleName = role.Name,
                    AssignedRoleCount = (await _user.GetUsersInRoleAsync(role.Name)).Count
                };

                roleViewModels.Add(roleViewModel);
            }

            return Ok(roleViewModels);
        }

        //api endpoint to create roles
        [HttpPost("CreateRole")]
        public async Task<IActionResult> Create([FromBody] IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                var result = await _role.CreateAsync(role);
                if (result.Succeeded)
                {
                    return Ok(new { Message = "Role created successfully" });
                }

                return BadRequest(new { Errors = result.Errors.Select(error => error.Description) });
            }
            return BadRequest(new { Message = "Invalid model state" });
        }

        //api endpoint to delete role
        [HttpDelete("DeleteRole/{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _role.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound(new { Message = "Role not found" });
            }

            // Check if the role is assigned to any users
            var usersWithRole = await _user.GetUsersInRoleAsync(role.Name);

            if (usersWithRole.Count > 0)
            {
                return BadRequest(new { Message = "Cannot delete role because it is assigned to one or more users." });
            }

            var result = await _role.DeleteAsync(role);
            if (result.Succeeded)
            {
                return Ok(new { Message = "Role deleted successfully" });
            }

            return BadRequest(new { Errors = result.Errors.Select(error => error.Description) });
        }
    }
}
