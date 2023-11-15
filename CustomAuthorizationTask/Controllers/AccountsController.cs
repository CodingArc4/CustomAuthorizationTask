using CustomAuthorizationTask.Data;
using CustomAuthorizationTask.Models;
using CustomAuthorizationTask.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CustomAuthorizationTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AccountsController(ApplicationDbContext context,SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

                var result = await _signInManager.CheckPasswordSignInAsync(user, loginViewModel.Password,true);

                if (user != null)
                {

                    if (result.Succeeded)
                    {
                        var token = GenerateJwtToken(user);
                        return Ok(new { token });
                    }
                }
                return BadRequest(new { message = "Invalid login attempt." });
            }
            return BadRequest(new { message = "Invalid model state" });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.RoleName);
                    var token = GenerateJwtToken(user);

                    return Ok(new { token });
                }
                return BadRequest(new { errors = result.Errors });
            }
            return BadRequest(new { message = "Invalid model state" });
        }

        //Generate Jwt Token
        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Permission", Permissions.Permissions.Products.View),
                new Claim("Permission", Permissions.Permissions.Products.Create),
                new Claim("Permission", Permissions.Permissions.Catagory.View)
            };

            foreach (var role in roles.ToList())
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Token"]));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    claims: claims,
                    issuer: "https://localhost:44327/",
                    audience: "https://localhost:44327/",
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}