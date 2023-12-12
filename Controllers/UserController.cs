using BE_WiseWallet.Entities;
using BE_WiseWallet.Entities.Respones;
using BE_WiseWallet.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace BE_WiseWallet.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserService _userService;
        public readonly ITokenService _tokenService;
        public readonly UserManager<ApplicationUser> _userManager;
        public UserController(IUserService userService, ITokenService tokenService, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _tokenService = tokenService;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserByAccessToken()
        {
            string accessToken = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            List<Claim> claims = _tokenService.GetClaimsFromExpiredToken(accessToken);
            string userId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            ApplicationUser user = await _userService.GetUserById(userId);
            string role = string.Join(",", await _userManager.GetRolesAsync(user)) == "Admin,User" ? "Admin" : "User";
            if (user == null)
            {
                return NotFound();
            }
            UserResponse u = new UserResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                FullName = user.Name,
                EmailAddress = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = role,
                Image = user.Image,
                Teams = user.Teams,
                IsRemoved = user.isRemoved,
            };
            return Ok(u);
        }
    }
}
