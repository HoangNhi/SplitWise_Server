using BE_WiseWallet.Entities;
using BE_WiseWallet.Entities.Requests;
using BE_WiseWallet.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BE_WiseWallet.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly IImageService _imageService;
        private readonly UserManager<ApplicationUser> _userManager;
        public TeamController(ITeamService teamService, IImageService imageService, UserManager<ApplicationUser> userManager)
        {
            _teamService = teamService;
            _imageService = imageService;
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _teamService.GetTeamById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewTeam([FromForm] TeamRequest teamRequest)
        {
            Image image = _imageService.Upload(teamRequest.Image).Result;
            Team team = new Team
            {
                LeaderId = teamRequest.LeaderId,
                Name = teamRequest.NameTeam,
                Image = image,
            };

            // Leader is a member of the team
            ApplicationUser Leader = await _userManager.FindByIdAsync(teamRequest.LeaderId.ToString());
            team.Members.Add(Leader);

            foreach (var member in teamRequest.MemberIds)
            {
                ApplicationUser Member = await _userManager.FindByIdAsync(member.ToString());
                team.Members.Add(Member);
            }

            return Ok(await _teamService.CreateNewTeam(team));
        }
    }
}
