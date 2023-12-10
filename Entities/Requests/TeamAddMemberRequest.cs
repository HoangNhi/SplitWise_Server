namespace BE_WiseWallet.Entities.Requests
{
    public class TeamAddMemberRequest
    {
        public int TeamId { get; set; }
        public ICollection<string> Members { get; set; }
    }
}
