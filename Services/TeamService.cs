using BE_WiseWallet.Data;
using BE_WiseWallet.Entities;
using BE_WiseWallet.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace BE_WiseWallet.Services
{
    public class TeamService : ITeamService
    {
        public readonly ApplicationDbContext _context;
        public TeamService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<Team> AddMember()
        {
            throw new NotImplementedException();
        }

        public async Task<Team> CreateNewTeam(Team team)
        {
            await _context.Teams.AddAsync(team);
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
