namespace BE_WiseWallet.Entities.Respones
{
    public class UserRespone
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public Image Image { get; set; }
        public ICollection<Team> Teams { get; set; }
    }
}
