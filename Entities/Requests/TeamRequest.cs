namespace BE_WiseWallet.Entities.Requests
{
    public class TeamRequest
    {
        public required string LeaderId { get; set; }
        public required string NameTeam { get; set; }
        public required virtual ICollection<string> MemberIds { get; set; } = new List<string>();
        public IFormFile Image { get; set; }
    }
}
