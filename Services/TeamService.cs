using BE_WiseWallet.Data;
using BE_WiseWallet.Entities;
using BE_WiseWallet.Entities.Requests;
using BE_WiseWallet.Services.IServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BE_WiseWallet.Services
{
    public class TeamService : ITeamService
    {
        public readonly ApplicationDbContext _context;
        public readonly UserManager<ApplicationUser> _userManager;
        public TeamService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<Team> AddMember(int TeamId, ICollection<string> Members)
        {
            // Leader is a member of the team
            Team team = await _context.Teams.FirstOrDefaultAsync(x => x.Id == TeamId);
            team.Members = new List<ApplicationUser>();

            foreach (var member in Members)
            {
                ApplicationUser Member = await _userManager.FindByIdAsync(member);
                _context.Entry(Member).State = EntityState.Unchanged;
                team.Members.Add(Member);
            }
            _context.Teams.Update(team);
            await _context.SaveChangesAsync();
            return team;
        }

        public async Task<Team> CreateNewTeam(Team team)
        {
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();
            return team;
        }

        public Task<Team> DeleteMember()
        {
            throw new NotImplementedException();
        }

        public Task<Team> GetTeamById(int Id)
        {
            return _context.Teams.FirstOrDefaultAsync(x => x.Id == Id);                                                                
        }
    }
}
