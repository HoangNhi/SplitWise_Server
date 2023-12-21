using BE_WiseWallet.Data;
using BE_WiseWallet.Entities;
using BE_WiseWallet.Entities.Requests;
using BE_WiseWallet.Entities.Requests.Team;
using BE_WiseWallet.Enums;
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
            Team team = await _context.Teams.Include(t => t.Members).FirstOrDefaultAsync(x => x.Id == TeamId);

            foreach (var member in Members)
            {
                Member checkMember = team.Members.FirstOrDefault(x => x.UserId == member);
                if (checkMember != null)
                {
                    checkMember.Status = MemberStatus.Active;
                    checkMember.UpdateAt = DateTime.Now;
                    _context.Members.Update(checkMember);
                    continue;
                }
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

        public Task<Team> CompleteTravel(int TeamId)
        {
            Team team = _context.Teams.FirstOrDefault(x => x.Id == TeamId);
            if(team == null)
            {
                return null;
            }
            team.isCompleted = true;
            _context.Teams.Update(team);
            _context.SaveChanges();
            return Task.FromResult(team);
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
                // Just update member active status
                Member memberToRemove = team.Members.Where(t => t.Status != MemberStatus.Inactive).FirstOrDefault(x => x.UserId == member);
                memberToRemove.Status = MemberStatus.Inactive;
                memberToRemove.UpdateAt = DateTime.Now;
                _context.Members.Update(memberToRemove);
            }
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
            Team team = _context.Teams.FirstOrDefault(x => x.Id == TeamId);
            Member member = team.Members.FirstOrDefault(x => x.UserId == UserId);
            if(member != null)
            {
                member.Status = MemberStatus.Inactive;
                member.UpdateAt = DateTime.Now;
                _context.Members.Update(member);
                _context.SaveChanges();
                return Task.FromResult(team);
            }
            else
            {
                return Task.FromResult<Team>(null);
            }
        }

        public Task<Team> UpdateTeam(TeamUpdate teamUpdate)
        {
            Team team = GetTeamById(teamUpdate.Id).Result;
            if(team == null)
            {
                return null;
            }
            else
            {
                team.LeaderId = teamUpdate.LeaderId == null ? team.LeaderId : teamUpdate.LeaderId;
                team.Name = teamUpdate.Name == null ? team.Name : teamUpdate.Name;
            }
            _context.Teams.Update(team);
            _context.SaveChanges();
            return Task.FromResult(team);
        }


    }
}
