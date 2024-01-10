using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagementApi.Authentication;
using TaskManagementApi.Common;

namespace TaskManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;

        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginModel registerModel)
        {
            var userExist = await userManager.FindByNameAsync(registerModel.UserName);
            if (userExist != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthSerivceResponse { Status = "Error", Message = "User already exist!"});

            var newUser = new ApplicationUser
            {
                UserName = registerModel.UserName,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var result = await userManager.CreateAsync(newUser, registerModel.Password);

            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            await userManager.AddToRoleAsync(newUser, UserRoles.User);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthSerivceResponse { Status = "Error", Message = "User creation failed! Please check user details and try again"});

            return Ok(new AuthSerivceResponse { Status = "Succes", Message = "User created succesfully"});
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = await userManager.FindByNameAsync(loginModel.UserName);
            if (user != null && await userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, loginModel.UserName),
                    new Claim("LogoutTime", (DateTime.UtcNow.AddMinutes(15)).ToString())
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]));

                var token = new JwtSecurityToken
                (
                    claims: authClaims,
                    audience: configuration.GetSection("Jwt:ValidAudience").Value,
                    issuer: configuration.GetSection("Jwt:ValidIssuer").Value,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            return Unauthorized();
        }

        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] LoginModel loginModel)
        {
            var userExist = await userManager.FindByNameAsync(loginModel.UserName);
            if (userExist != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthSerivceResponse { Status = "Error", Message = "User alredy exist!" });

            var newUser = new ApplicationUser
            {
                UserName = loginModel.UserName,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var result = await userManager.CreateAsync(newUser, loginModel.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthSerivceResponse { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

            if (await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await userManager.AddToRoleAsync(newUser, UserRoles.Admin);
            }

            return Ok(new AuthSerivceResponse { Status = "Success", Message = "User created successfully!" });
        }
    }
}
