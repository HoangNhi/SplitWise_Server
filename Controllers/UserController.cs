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
            UserResponse userResponse = await _userService.GetUserByAccessToken(accessToken);
            if (userResponse == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(userResponse);
            }
        }
    }
}
