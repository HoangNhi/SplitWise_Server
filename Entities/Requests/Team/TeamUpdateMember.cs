namespace BE_WiseWallet.Entities.Requests.Team
{
    public class TeamUpdateMember
    {
        public required int TeamId { get; set; }
        public required ICollection<string> Members { get; set; }
    }
}
