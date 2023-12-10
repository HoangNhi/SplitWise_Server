using BE_WiseWallet.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_WiseWallet.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetUserById(string Id)
        {
            var user = await _userService.GetUserById(Id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
