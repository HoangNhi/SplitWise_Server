namespace BE_WiseWallet.Entities.Respones
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public Boolean IsRemoved { get; set; }
        public Image Image { get; set; }
        public ICollection<Member> Teams { get; set; }
        public string Role { get; set; }

    }
}
