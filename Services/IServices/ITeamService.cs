using BE_WiseWallet.Entities;

namespace BE_WiseWallet.Services.IServices
{
    public interface ITeamService
    {
        public Task<Team> GetTeamById(int Id);
        public Task<Team> CreateNewTeam(Team team);
        public Task<Team> DeleteMember();
        public Task<Team> AddMember();
    }
}
