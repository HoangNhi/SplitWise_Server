namespace BE_WiseWallet.Entities.Requests.Team
{
    public class TeamCreate
    {
        public required string LeaderId { get; set; }
        public required string NameTeam { get; set; }
        public required virtual ICollection<string> MemberIds { get; set; } = new List<string>();
        public IFormFile Image { get; set; }
    }
}
