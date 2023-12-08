namespace BE_WiseWallet.Entities.Requests
{
    public class TeamRequest
    {
        public required int LeaderId { get; set; }
        public required string NameTeam { get; set; }
        public required virtual ICollection<int> MemberIds { get; set; } = new List<int>();
        public IFormFile Image { get; set; }
    }
}
