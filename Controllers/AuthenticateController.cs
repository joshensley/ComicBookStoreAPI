using ComicBookStoreAPI.Models;
using ComicBookStoreAPI.Models.Auth;
using ComicBookStoreAPI.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticateController(
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            var userExists = await _userManager.FindByEmailAsync(registerModel.Email);
            if (userExists != null)
            {
                var responseModels = new List<ResponseModel>() 
                { 
                    new ResponseModel() { Status = "Error", Message = "User already exists!" }
                };

                return StatusCode(StatusCodes.Status500InternalServerError,responseModels);
            }

            ApplicationUser user = new ApplicationUser()
            {
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                UserName = registerModel.Email,
                Email = registerModel.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (!result.Succeeded)
            {
                var responseModels = new List<ResponseModel>();
                foreach (var item in result.Errors)
                {
                    var error = new ResponseModel()
                    {
                        Status = "Error",
                        Message = item.Description
                    };

                    responseModels.Add(error);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, responseModels);
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }

            await _userManager.AddToRoleAsync(user, UserRoles.User);

            var successResponseModels = new List<ResponseModel>()
            {
                new ResponseModel() { Status = "Success", Message = "User created successfully!" }
            };

            return Ok(successResponseModels);
        }

        [HttpPost]
        [Route("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel registerModel)
        {
            var userExists = await _userManager.FindByEmailAsync(registerModel.Email);
            if (userExists != null)
            {
                var responseModels = new List<ResponseModel>()
                {
                    new ResponseModel() { Status = "Error", Message = "User already exists!" }
                };

                return StatusCode(StatusCodes.Status500InternalServerError, responseModels);
            }

            ApplicationUser user = new ApplicationUser()
            {
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                UserName = registerModel.Email,
                Email = registerModel.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (!result.Succeeded)
            {
                var responseModels = new List<ResponseModel>();
                foreach (var item in result.Errors)
                {
                    var error = new ResponseModel()
                    {
                        Status = "Error",
                        Message = item.Description
                    };

                    responseModels.Add(error);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, responseModels);
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }

            await _userManager.AddToRoleAsync(user, UserRoles.Admin);

            var successResponseModels = new List<ResponseModel>()
            {
                new ResponseModel() { Status = "Success", Message = "Admin created successfully!" }
            };

            return Ok(successResponseModels);
        }
    }
}
