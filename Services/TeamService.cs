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
            team.Members = new List<Member>();

            foreach (var member in Members)
            {
                Member NewMember = new Member
                {
                    TeamId = TeamId,
                    Team = team,
                    UserId = member,
                    User = await _userManager.FindByIdAsync(member)
                };
                _context.Entry(NewMember.User).State = EntityState.Unchanged;
                team.Members.Add(NewMember);
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

        public Task<Team> DeleteMember(Team team, ICollection<string> Members)
        {
            foreach (var member in Members)
            {
                Member memberToRemove = team.Members.FirstOrDefault(x => x.UserId == member);
                team.Members.Remove(memberToRemove);
            }
            _context.Teams.Update(team);
            _context.SaveChanges();
            return Task.FromResult(team);
        }

        public Task<Team> GetTeamById(int Id)
        {
            return _context.Teams
                           .Include(t => t.Members)
                           .ThenInclude(m => m.User)
                           .FirstOrDefaultAsync(x => x.Id == Id);                                                                
        }

        public Task<Team> OutTeam(int TeamId, string UserId)
        {
            throw new NotImplementedException();
        }
    }
}
