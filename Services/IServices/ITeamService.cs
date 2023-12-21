using BE_WiseWallet.Entities;
using BE_WiseWallet.Entities.Requests.Team;

namespace BE_WiseWallet.Services.IServices
{
    public interface ITeamService
    {
        public Task<Team> GetTeamById(int Id);
        public Task<Team> CreateNewTeam(Team team);
        public Task<Team> DeleteMember(Team team, ICollection<string> Members);
        public Task<Team> AddMember(int TeamId, ICollection<string> Members);
        public Task<Team> OutTeam(int TeamId, string UserId);
        public Task<Team> UpdateTeam(TeamUpdate teamUpdate);
        public Task<Team> CompleteTravel(int TeamId);
    }
}
