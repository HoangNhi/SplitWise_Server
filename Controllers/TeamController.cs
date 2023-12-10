using BE_WiseWallet.Data;
using BE_WiseWallet.Entities;
using BE_WiseWallet.Entities.Requests;
using BE_WiseWallet.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        private readonly ApplicationDbContext _context;
        public TeamController(ITeamService teamService, IImageService imageService, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _teamService = teamService;
            _imageService = imageService;
            _userManager = userManager;
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _teamService.GetTeamById(id));
        }

        [HttpPost("CreateNewTeam")]
        public async Task<IActionResult> CreateNewTeam([FromForm] TeamRequest teamRequest)
        {
            Image image = _imageService.Upload(teamRequest.Image).Result;
            Team team = new Team
            {
                LeaderId = teamRequest.LeaderId,
                Name = teamRequest.NameTeam,
                Image = image,
                Members = new List<ApplicationUser>(),
            };

            teamRequest.MemberIds.Add(teamRequest.LeaderId);

            await _teamService.CreateNewTeam(team);
            await _teamService.AddMember(team.Id, teamRequest.MemberIds);
            return Ok(team);
        }

        [HttpPost("AddMember")]
        public async Task<IActionResult> AddMember([FromForm] TeamAddMemberRequest request)
        {
            await _teamService.AddMember(request.TeamId, request.Members);
            return Ok();
        }
    }
}
