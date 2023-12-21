using BE_WiseWallet.Data;
using BE_WiseWallet.Entities;
using BE_WiseWallet.Entities.Respones;
using BE_WiseWallet.Services.IServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BE_WiseWallet.Services
{
    public class UserService : IUserService
    {
        public readonly ApplicationDbContext _context;
        public readonly ITokenService _tokenService;
        public readonly UserManager<ApplicationUser> _userManager;
        public readonly ITeamService _teamService;
        public UserService(ApplicationDbContext context, ITokenService tokenService, UserManager<ApplicationUser> userManager, ITeamService teamService)
        {
            _context = context;
            _tokenService = tokenService;
            _userManager = userManager;
            _teamService = teamService;
        }

        public async Task<UserResponse> GetUserByAccessToken(string token)
        {
            List<Claim> claims = _tokenService.GetClaimsFromExpiredToken(token);
            string userId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            ApplicationUser user = GetUserById(userId).Result;
            List<Team> teams = new List<Team>(); 
            foreach(var team in user.Teams)
            {
                teams.Add(_teamService.GetTeamById(team.TeamId).Result);   
            }
            string role = string.Join(",", await _userManager.GetRolesAsync(user)) == "Admin,User" ? "Admin" : "User";
            if (user == null)
            {
                return null;
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
                Teams = teams,
                IsRemoved = user.isRemoved,
            };
            return u;
        }

        public Task<ApplicationUser> GetUserById(string Id)
        {
           return _context.Users
                          .Include(u => u.Teams)
                          .Include(u => u.Image)
                          .SingleOrDefaultAsync(u => u.Id == Id);
        }
    }
}
