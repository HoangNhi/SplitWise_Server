using BE_WiseWallet.Data;
using BE_WiseWallet.Entities;
using BE_WiseWallet.Entities.Requests.Team;
using BE_WiseWallet.Entities.Respones;
using BE_WiseWallet.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
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
        private readonly IUserService _userService;
        public TeamController(ITeamService teamService, IImageService imageService, IUserService userService)
        {
            _teamService = teamService;
            _imageService = imageService;
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Team team = await _teamService.GetTeamById(id);
            if (team == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(team);
            }
        }

        [HttpPost("CreateNewTeam")]
        public async Task<IActionResult> CreateNewTeam([FromForm] TeamCreate teamRequest)
        {
            try
            {
                Image image = _imageService.Upload(teamRequest.Image).Result;
                Team team = new Team
                {
                    LeaderId = teamRequest.LeaderId,
                    Name = teamRequest.NameTeam,
                    Image = image,
                    Members = new List<Member>(),
                };

                teamRequest.MemberIds.Add(teamRequest.LeaderId);

                Team NewTeam = await _teamService.CreateNewTeam(team);
                await _teamService.AddMember(NewTeam.Id, teamRequest.MemberIds);
                return Ok(team);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("AddMember")]
        public async Task<IActionResult> AddMember([FromForm] TeamUpdateMember request)
        {
            try
            {
                Team team = await _teamService.AddMember(request.TeamId, request.Members);
                return Ok(team);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("DeleteMember")]
        public async Task<IActionResult> DeleteMember([FromForm] TeamUpdateMember request)
        {
            string AccessToken = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            UserResponse UserResponse = await _userService.GetUserByAccessToken(AccessToken);
            Team team = await _teamService.GetTeamById(request.TeamId);
            
            if (UserResponse.Id != team.LeaderId)
            {
                return new ObjectResult(new { message = "You are not the leader of this team" }) { StatusCode = StatusCodes.Status403Forbidden };
            }
            else if (request.Members.Contains(team.LeaderId))
            {
                return new ObjectResult(new { message = "Leader can't leave the team" }) { StatusCode = StatusCodes.Status405MethodNotAllowed };
            }
            else
            {
                Team t = await _teamService.DeleteMember(team, request.Members);
                if(t == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(t);
                }
            }
        }

        [HttpDelete("OutTeam")]
        public async Task<IActionResult> OutTeam([FromForm] int id)
        {
            string AccessToken = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            UserResponse UserResponse = await _userService.GetUserByAccessToken(AccessToken);
            Team team = UserResponse.Teams.FirstOrDefault(t => t.Id == id);
            if (team == null)
            {
                return BadRequest();
            }
            else if(UserResponse.Id == team.LeaderId)
            {
                return new ObjectResult(new { message = "Leader can't leave the team" }) { StatusCode = StatusCodes.Status405MethodNotAllowed };
            }   
            else
            {
                Team t = await _teamService.OutTeam(id, UserResponse.Id);
                if (t == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(t);
                }
            }
        }

        [HttpPut("UpdateTeam")]
        public async Task<IActionResult> UpdateTeam([FromForm] TeamUpdate teamRequest)
        {
            try
            {
                string AccessToken = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
                UserResponse UserResponse = await _userService.GetUserByAccessToken(AccessToken);
                Team UpdateTeam = await _teamService.GetTeamById(teamRequest.Id);

                if(UserResponse.Id != UpdateTeam.LeaderId)
                {
                    return new ObjectResult(new { message = "You are not the leader of this team" }) { StatusCode = StatusCodes.Status403Forbidden };
                }

                Team team = await _teamService.UpdateTeam(teamRequest);
                if(team == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(team);
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("CompleteTravel")]
        public async Task<IActionResult> CompleteTravel(int TeamId)
        {
            try
            {
                string AccessToken = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
                UserResponse UserResponse = await _userService.GetUserByAccessToken(AccessToken);
                Team UpdateTeam = await _teamService.GetTeamById(TeamId);

                if (UserResponse.Id != UpdateTeam.LeaderId)
                {
                    return new ObjectResult(new { message = "You are not the leader of this team" }) { StatusCode = StatusCodes.Status403Forbidden };
                }
                else
                {
                    return Ok(await _teamService.CompleteTravel(TeamId));
                }
            }
            catch
            { 
                return BadRequest();
            }
        }
    }
}
